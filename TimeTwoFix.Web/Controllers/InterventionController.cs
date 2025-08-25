using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTwoFix.Application.InterventionService.Dtos;
using TimeTwoFix.Application.InterventionService.Interfaces;
using TimeTwoFix.Application.LiftingBridgeServices.Interfaces;
using TimeTwoFix.Application.ProvidedServicesService.Interfaces;
using TimeTwoFix.Application.UserServices.Interfaces;
using TimeTwoFix.Application.WorkOrderService.Interfaces;
using TimeTwoFix.Core.Entities.WorkOrderManagement;
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
        public InterventionController(IInterventionService interventionService, IMapper mapper, IWorkOrderService workOrderService, IProvidedServiceService providedServiceService,
            IUserService userService, ILiftingBridgeServices liftingBridgeServices) : base(interventionService, mapper)
        {
            _interventionService = interventionService;
            _workOrderService = workOrderService;
            _userService = userService;
            _providedServiceService = providedServiceService;
            _liftingBridgeServices = liftingBridgeServices;

        }

        [HttpGet]
        public override async Task<ActionResult> Create()
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





    }
}
