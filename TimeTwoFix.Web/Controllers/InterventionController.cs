using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTwoFix.Application.Base.BaseDtos;
using TimeTwoFix.Application.InterventionService.Dtos;
using TimeTwoFix.Application.InterventionService.Interfaces;
using TimeTwoFix.Application.LiftingBridgeServices.Interfaces;
using TimeTwoFix.Application.ProvidedServicesService.Interfaces;
using TimeTwoFix.Application.UserServices.Interfaces;
using TimeTwoFix.Application.WorkOrderService.Interfaces;
using TimeTwoFix.Core.Entities.WorkOrderManagement;
using TimeTwoFix.Core.Interfaces;
using TimeTwoFix.Infrastructure.Persistence.Includes;
using TimeTwoFix.Web.Models.InterventionModels;

namespace TimeTwoFix.Web.Controllers
{

    public class InterventionController : BaseController<Intervention, CreateInterventionDto, ReadInterventionDto, UpdateInterventionDto, DeleteInterventionDto,
        CreateInterventionViewModel, ReadInterventionViewModel, UpdateInterventionViewModel, DeleteInterventionViewModel>

    {
        private readonly IInterventionService _interventionService;
        private readonly IWorkOrderService _workOrderService;
        private readonly IProvidedServiceService _providedServiceService;
        private readonly IUserService _userService;
        private readonly ILiftingBridgeServices _liftingBridgeServices;
        private readonly IUnitOfWork _unitOfWork;
        public InterventionController(IInterventionService interventionService, IMapper mapper, IWorkOrderService workOrderService, IProvidedServiceService providedServiceService,
            IUserService userService, ILiftingBridgeServices liftingBridgeServices, IUnitOfWork unitOfWork) : base(interventionService, mapper)
        {
            _interventionService = interventionService;
            _workOrderService = workOrderService;
            _userService = userService;
            _providedServiceService = providedServiceService;
            _liftingBridgeServices = liftingBridgeServices;
            _unitOfWork = unitOfWork;

        }

        [HttpGet]
        public override async Task<IActionResult> Index()
        {
            // Redirect to PaginatedIndex to handle the status and pagination logic
            return RedirectToAction("PaginatedIndex", "Intervention");
        }


        [HttpGet]
        public async Task<IActionResult> PaginatedIndex(string status = null, int page = 1, int pageSize = 100)
        {
            var includes = EntityIncludeHelper.GetIncludes<Intervention>();
            // Get status counts for tabs
            var statusCountsRaw = await _interventionService.GroupCountAsynServiceGeneric(i => i.Status);
            var statusCounts = _mapper.Map<IReadOnlyList<StatusCountDto>>(statusCountsRaw);

            // If no status is provided, default to "In Progress" or first available status
            if (string.IsNullOrEmpty(status) && statusCounts.Any())
            {
                status = statusCounts.FirstOrDefault(s => s.Status == "In Progress")?.Status
                        ?? statusCounts.First().Status;
            }

            var skip = (page - 1) * pageSize;
            var pagedInterventions = await _interventionService.GetPagedByPredicateAsyncServiceGeneric(
                i => i.Status == status,
                skip,
                pageSize,
                i => i.CreatedAt,
                descending: true, includes);


            var totalCount = await _interventionService.GetCountByPredicateAsyncServiceGeneric(i => i.Status == status);



            var interventionDtos = _mapper.Map<List<ReadInterventionDto>>(pagedInterventions);
            var viewModels = _mapper.Map<List<ReadInterventionViewModel>>(interventionDtos);

            ViewBag.StatusCounts = statusCounts;
            ViewBag.CurrentStatus = status;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return View(viewModels);
        }




        [HttpGet]
        public override async Task<ActionResult> Create()
        {
            var activeWorkOrders = (await _workOrderService.GetAllAsyncServiceGeneric()).Where(w => !w.IsDeleted && w.Status != "Completed");
            var activeProvidedServices = (await _providedServiceService.GetAllAsyncServiceGeneric()).Where(ps => !ps.IsDeleted);
            var activeMechanics = (await _userService.GetAllApplicationUsers()).Where(u => u.UserType.Equals("Mechanic"));
            var activeLiftingBridges = (await _liftingBridgeServices.GetAllAsyncServiceGeneric()).Where(lb => !lb.IsDeleted);
            ViewBag.WorkOrder = activeWorkOrders.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Id.ToString()
            }).ToList();
            ViewBag.ProvidedService = activeProvidedServices.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            ViewBag.Mechanic = new SelectList(activeMechanics.Select(c => new
            {
                c.Id,
                FullName = c.FirstName + " " + c.LastName
            }), "Id", "FullName");
            ViewBag.LiftingBridge = activeLiftingBridges.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();



            return View();
        }
        [HttpPost]
        public override async Task<IActionResult> Create(CreateInterventionViewModel viewModel)
        {
            var activeWorkOrders = (await _workOrderService.GetAllAsyncServiceGeneric()).Where(w => !w.IsDeleted);
            var activeProvidedServices = (await _providedServiceService.GetAllAsyncServiceGeneric()).Where(ps => !ps.IsDeleted);
            var activeMechanics = (await _userService.GetAllApplicationUsers()).Where(u => u.UserType.Equals("Mechanic"));
            var activeLiftingBridges = (await _liftingBridgeServices.GetAllAsyncServiceGeneric()).Where(lb => !lb.IsDeleted);
            ViewBag.WorkOrder = activeWorkOrders.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Id.ToString()
            }).ToList();
            ViewBag.ProvidedService = activeProvidedServices.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            ViewBag.Mechanic = new SelectList(activeMechanics.Select(c => new
            {
                c.Id,
                FullName = c.FirstName + " " + c.LastName
            }), "Id", "FullName");
            ViewBag.LiftingBridge = activeLiftingBridges.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid data provided";
                return View(viewModel);
            }
            try
            {
                var dto = _mapper.Map<CreateInterventionDto>(viewModel);
                dto.CreatedBy = User.Identity.Name; // Set the created by user
                dto.CreatedAt = DateTime.Now; // Set the creation date

                var entity = _mapper.Map<Intervention>(dto);
                entity.Status = "In Progress"; // Set default status
                await _interventionService.AddAsyncServiceGeneric(entity);
                TempData["SuccessMessage"] = "Intervention created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while creating the intervention: {ex.Message}";
                return View(viewModel);
            }
        }

        [HttpGet]
        public override Task<IActionResult> Edit(int id)
        {
            return base.Edit(id);
        }
        [HttpPost]
        public override async Task<IActionResult> Edit(int id, UpdateInterventionViewModel viewModel)
        {
            var includesIntervention = EntityIncludeHelper.GetIncludes<Intervention>();
            var includesWorkOrder = EntityIncludeHelper.GetIncludes<WorkOrder>();

            var intervention = await _interventionService.GetByIdAsyncServiceGeneric(id, includesIntervention);




            if (intervention == null)
            {
                TempData["ErrorMessage"] = "Intervention not found";
                return NotFound();
            }

            try
            {
                var dto = _mapper.Map<UpdateInterventionDto>(viewModel);
                var updatedEntity = _mapper.Map(dto, intervention);
                if (updatedEntity.EndDate != null)
                {
                    var timeSpent = updatedEntity.CalculateActualTimeSpent();
                    updatedEntity.InterventionPrice = (decimal)timeSpent.TotalHours * updatedEntity.Service.PricePerHour;
                }
                if (updatedEntity.EndDate != null)
                {
                    updatedEntity.Status = "Completed";
                }
                await _interventionService.UpdateAsyncServiceGeneric(updatedEntity);
                await _workOrderService.DetachAsyncServiceGeneric(updatedEntity.WorkOrder);

                var workOrder = await _workOrderService.GetByIdAsyncServiceGeneric(updatedEntity.WorkOrderId, includesWorkOrder);


                workOrder.RecalculateLaborCost();
                workOrder.UpdatedAt = DateTime.Now;
                workOrder.UpdatedBy = User.Identity?.Name;

                await _workOrderService.UpdateAsyncServiceGeneric(workOrder);

                TempData["SuccessMessage"] = "Intervention updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while updating the intervention: {ex.Message}";
                return View(viewModel);
            }


        }
    }
}
