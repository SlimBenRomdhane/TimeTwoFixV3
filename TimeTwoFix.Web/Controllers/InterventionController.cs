using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTwoFix.Application.Base.BaseDtos;
using TimeTwoFix.Application.InterventionService.Dtos;
using TimeTwoFix.Application.InterventionService.Interfaces;
using TimeTwoFix.Application.LiftingBridgeServices.Interfaces;
using TimeTwoFix.Application.PauseRecordService.Interfaces;
using TimeTwoFix.Application.ProvidedServicesService.Interfaces;
using TimeTwoFix.Application.UserServices.Interfaces;
using TimeTwoFix.Application.WorkOrderService.Interfaces;
using TimeTwoFix.Core.Entities.WorkOrderManagement;
using TimeTwoFix.Core.Interfaces;
using TimeTwoFix.Infrastructure.Persistence.Includes;
using TimeTwoFix.Web.Models.InterventionModels;
using TimeTwoFix.Web.Models.PauseRecordModel;

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
        private readonly IPauseRecordService _pauseRecordService;
        private readonly IUnitOfWork _unitOfWork;

        public InterventionController(IInterventionService interventionService, IMapper mapper, IWorkOrderService workOrderService, IProvidedServiceService providedServiceService,
            IUserService userService, ILiftingBridgeServices liftingBridgeServices, IUnitOfWork unitOfWork, IPauseRecordService pauseRecordService) : base(interventionService, mapper)
        {
            _interventionService = interventionService;
            _workOrderService = workOrderService;
            _userService = userService;
            _providedServiceService = providedServiceService;
            _liftingBridgeServices = liftingBridgeServices;
            _pauseRecordService = pauseRecordService;
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
            try
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
                if (pagedInterventions == null)
                {
                    TempData["ErrorMessage"] = "No interventions found.";
                    return View(new List<ReadInterventionViewModel>());
                }
                else
                {
                    var interventionDtos = _mapper.Map<List<ReadInterventionDto>>(pagedInterventions);
                    var viewModels = _mapper.Map<List<ReadInterventionViewModel>>(interventionDtos);
                    TempData["SuccessMessage"] = $"Loaded {viewModels.Count} interventions for status '{status}'.";
                    ViewBag.StatusCounts = statusCounts;
                    ViewBag.CurrentStatus = status;
                    ViewBag.CurrentPage = page;
                    ViewBag.PageSize = pageSize;
                    ViewBag.TotalCount = totalCount;
                    ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                    return View(viewModels);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while loading interventions: {ex.Message}";
                return View(new List<ReadInterventionViewModel>());
            }
        }

        [HttpGet]
        public override async Task<ActionResult> Create()
        {
            try
            {
                var activeWorkOrders = (await _workOrderService.GetAllAsyncServiceGeneric()).Where(w => !w.IsDeleted && w.Status != "Completed"
            && w.Status != "Canceled");
                var activeProvidedServices = (await _providedServiceService.GetAllAsyncServiceGeneric()).Where(ps => !ps.IsDeleted);
                var activeMechanics = (await _userService.GetAllApplicationUsers()).Where(u => u.UserType.Equals("Mechanic"));
                var activeLiftingBridges = (await _liftingBridgeServices.GetAllAsyncServiceGeneric()).Where(lb => !lb.IsDeleted);
                // Check for missing dependencies
                if (!activeWorkOrders.Any() || !activeProvidedServices.Any() || !activeMechanics.Any() || !activeLiftingBridges.Any())
                {
                    TempData["ErrorMessage"] = "Some required data is missing. Please check work orders, services, mechanics, or lifting bridges.";
                }
                else
                {
                    TempData["SuccessMessage"] = "All dependencies loaded successfully.";
                }

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
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while preparing the form: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
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
                if (DateTime.Now < entity.StartDate)
                {
                    entity.Status = "Planned";
                }
                else
                {
                    entity.Status = "In Progress"; // Set default status
                }

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
            var intervention = await _interventionService.GetByIdAsyncServiceGeneric(id, null, includesIntervention);
            if (intervention == null)
            {
                TempData["ErrorMessage"] = "Intervention not found";
                return RedirectToAction(nameof(Index));
                //return NotFound();
            }

            try
            {
                var dto = _mapper.Map<UpdateInterventionDto>(viewModel);
                var updatedEntity = _mapper.Map(dto, intervention);
                if (updatedEntity.EndDate != null)
                {
                    var timeSpent = updatedEntity.CalculateActualTimeSpent();
                    updatedEntity.InterventionPrice = (decimal)timeSpent.TotalHours * updatedEntity.Service.PricePerHour;
                    updatedEntity.Status = "Completed";
                }
                await _interventionService.UpdateAsyncServiceGeneric(updatedEntity);
                await _workOrderService.DetachAsyncServiceGeneric(updatedEntity.WorkOrder);

                var workOrder = await _workOrderService.GetByIdAsyncServiceGeneric(updatedEntity.WorkOrderId, null, includesWorkOrder);
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

        [HttpPost]
        public async Task<IActionResult> PauseIntervention(CreatePauseRecordViewModel createPauseRecordViewModel)
        {
            var includes = EntityIncludeHelper.GetIncludes<Intervention>();
            var intervention = await _interventionService.GetByIdAsyncServiceGeneric(createPauseRecordViewModel.InterventionId, null, includes);
            if (intervention == null)
            {
                return NotFound(new { message = "Intervention not found" });
            }
            if (intervention.Status != "In Progress")
            {
                return BadRequest(new { message = "Only interventions that are 'In Progress' can be paused." });
            }
            try
            {
                // Create a new PauseRecord
                var pauseRecord = new PauseRecord
                {
                    Reason = createPauseRecordViewModel.Reason,
                    StartTime = DateTime.Now,
                    InterventionId = intervention.Id
                };
                // Update the intervention status to "Paused"
                intervention.Status = "Paused";
                // Save changes
                await _pauseRecordService.AddAsyncServiceGeneric(pauseRecord);
                await _interventionService.UpdateAsyncServiceGeneric(intervention);

                //return Ok(new { message = "Intervention paused successfully." });
                return RedirectToAction("PaginatedIndex");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while pausing the intervention: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResumeIntervention(int interventionId)
        {
            var includes = EntityIncludeHelper.GetIncludes<Intervention>();
            var intervention = await _interventionService.GetByIdAsyncServiceGeneric(interventionId, null, includes);

            if (intervention == null)
                return NotFound(new { message = "Intervention not found" });

            if (intervention.Status != "Paused")
                return BadRequest(new { message = "Only paused interventions can be resumed." });

            var activePause = intervention.PauseRecords
                .FirstOrDefault(p => p.EndTime == null);

            if (activePause == null)
                return BadRequest(new { message = "No active pause found for this intervention." });

            try
            {
                activePause.EndTime = DateTime.Now;

                // Resume to "In Progress" — or use your own logic if completion is externally tracked
                intervention.Status = "In Progress";

                await _pauseRecordService.UpdateAsyncServiceGeneric(activePause);
                await _interventionService.UpdateAsyncServiceGeneric(intervention);

                //return Ok(new { message = "Intervention resumed successfully." });
                return RedirectToAction("PaginatedIndex");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while resuming the intervention: {ex.Message}" });
            }
        }

        public async Task<ActionResult> CreateById(int workOrderId)
        {
            var activeProvidedServices = (await _providedServiceService.GetAllAsyncServiceGeneric()).Where(ps => !ps.IsDeleted);
            var activeMechanics = (await _userService.GetAllApplicationUsers()).Where(u => u.UserType.Equals("Mechanic"));
            var activeLiftingBridges = (await _liftingBridgeServices.GetAllAsyncServiceGeneric()).Where(lb => !lb.IsDeleted);

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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateById(CreateInterventionViewModel createInterventionViewModel)
        {
            var activeProvidedServices = (await _providedServiceService.GetAllAsyncServiceGeneric()).Where(ps => !ps.IsDeleted);
            var activeMechanics = (await _userService.GetAllApplicationUsers()).Where(u => u.UserType.Equals("Mechanic"));
            var activeLiftingBridges = (await _liftingBridgeServices.GetAllAsyncServiceGeneric()).Where(lb => !lb.IsDeleted);

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

            try
            {
                var interventionDto = _mapper.Map<CreateInterventionDto>(createInterventionViewModel);
                interventionDto.CreatedBy = User.Identity?.Name;
                interventionDto.CreatedAt = DateTime.Now;

                var intervention = _mapper.Map<Intervention>(interventionDto);
                if (DateTime.Now < intervention.StartDate)
                {
                    intervention.Status = "Planned";
                }
                else
                {
                    intervention.Status = "In Progress"; // Set default status
                }
                await _interventionService.AddAsyncServiceGeneric(intervention);
                TempData["InterventionSuccess"] = "Intervention created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["WorkOrderError"] = $"An error occurred while creating the Work Order: {ex.Message}";
                return View(createInterventionViewModel);
            }
        }
    }
}