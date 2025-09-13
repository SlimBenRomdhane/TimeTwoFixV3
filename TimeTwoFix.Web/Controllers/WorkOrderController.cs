using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Application.VehicleServices.Interfaces;
using TimeTwoFix.Application.WorkOrderService.Dtos;
using TimeTwoFix.Application.WorkOrderService.Interfaces;
using TimeTwoFix.Core.Entities.WorkOrderManagement;
using TimeTwoFix.Web.Models.VehicleModels;
using TimeTwoFix.Web.Models.WorkOrderModels;

namespace TimeTwoFix.Web.Controllers
{
    [Authorize(Roles = "GeneralManager, WorkshopManager, FrontDeskAssistant")]
    public class WorkOrderController : Controller
    {
        private readonly IWorkOrderService _workOrderService;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public WorkOrderController(IWorkOrderService workOrderService, IMapper mapper, IVehicleService vehicleService)
        {
            _workOrderService = workOrderService;
            _mapper = mapper;
            _vehicleService = vehicleService;
        }

        // GET: WorkOrderController
        public async Task<ActionResult> Index()
        {
            //var workOrders = await _workOrderService.GetAllWithIncludesAsyncServiceGeneric(wo => wo.Vehicle, wo => wo.Interventions);
            var workOrders = await _workOrderService.GetAllWithIncludesAsyncServiceGeneric(includeBuilder: query => query
            .Include(wo => wo.Vehicle)
            .Include(wo => wo.Interventions).ThenInclude(inter => inter.InterventionSpareParts).ThenInclude(isp => isp.SparePart)

            );
            if (workOrders == null || !workOrders.Any())
            {
                TempData["WorkOrderError"] = "No WorkOrder found in the database";
                return View(Enumerable.Empty<ReadWorkOrderViewModel>());
            }
            workOrders = workOrders.Where(wo => !wo.IsDeleted);
            foreach (var workOrder in workOrders)
            {
                workOrder.RecalculateLaborCost();
                workOrder.UpdateStatus2();
            }
            await _workOrderService.SaveChangesServiceGeneric();
            var workOrderDtos = _mapper.Map<IEnumerable<ReadWorkOrderDto>>(workOrders);
            var workOrderViewModels = _mapper.Map<IEnumerable<ReadWorkOrderViewModel>>(workOrderDtos);

            return View(workOrderViewModels);
        }

        // GET: WorkOrderController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var workOrder = await _workOrderService.GetByIdAsyncServiceGeneric(id, includeBuilder: query => query
            .Include(wo => wo.Vehicle)
            .Include(wo => wo.Interventions)
                .ThenInclude(inter => inter.Service)
            .Include(wo => wo.Interventions)
                .ThenInclude(inter => inter.Mechanic));
            if (workOrder == null)
            {
                TempData["WorkOrderError"] = "WorkOrder not found";
                return RedirectToAction(nameof(Index));
            }
            var workOrderDto = _mapper.Map<ReadWorkOrderDto>(workOrder);
            var workOrderViewModel = _mapper.Map<ReadWorkOrderViewModel>(workOrderDto);
            return View(workOrderViewModel);
        }

        // GET: WorkOrderController/Create
        public async Task<ActionResult> Create()
        {
            var vehicles = await _vehicleService.GetAllAsyncServiceGeneric();
            ViewBag.VehicleList = new SelectList(vehicles, "Id", "Vin");

            return View();
        }

        // POST: WorkOrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateWorkOrderViewModel createWorkOrderViewModel)
        {
            var vehicles = await _vehicleService.GetAllAsyncServiceGeneric();
            ViewBag.VehicleList = new SelectList(vehicles, "Id", "Vin");
            if (!ModelState.IsValid)
            {
                TempData["WorkOrderError"] = "Invalid data provided";
                return View(createWorkOrderViewModel);
            }
            try
            {
                var workOrderDto = _mapper.Map<CreateWorkOrderDto>(createWorkOrderViewModel);
                workOrderDto.CreatedBy = User.Identity?.Name;
                var workOrder = _mapper.Map<WorkOrder>(workOrderDto);
                await _workOrderService.AddAsyncServiceGeneric(workOrder);
                TempData["WorkOrderSuccess"] = "Work Order created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["WorkOrderError"] = $"An error occurred while creating the Work Order: {ex.Message}";
                return View(createWorkOrderViewModel);
            }
        }

        public async Task<ActionResult> CreateById(int vehicleId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateById(CreateWorkOrderViewModel createWorkOrderViewModel)
        {
            try
            {
                var workOrderDto = _mapper.Map<CreateWorkOrderDto>(createWorkOrderViewModel);
                workOrderDto.CreatedBy = User.Identity?.Name;
                var workOrder = _mapper.Map<WorkOrder>(workOrderDto);
                await _workOrderService.AddAsyncServiceGeneric(workOrder);
                TempData["WorkOrderSuccess"] = "Work Order created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["WorkOrderError"] = $"An error occurred while creating the Work Order: {ex.Message}";
                return View(createWorkOrderViewModel);
            }
        }

        // GET: WorkOrderController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var workOrder = await _workOrderService.GetByIdAsyncServiceGeneric(id);
            if (workOrder == null)
            {
                TempData["WorkOrderError"] = "Work Order not found";
                return RedirectToAction(nameof(Index));
            }
            var allowedStatus = new List<SelectListItem>
            {
                new SelectListItem { Value = WorkOrderStatus.Paused.ToString(), Text = WorkOrderStatus.Paused.ToString() },
                new SelectListItem { Value = WorkOrderStatus.Canceled.ToString(), Text = WorkOrderStatus.Canceled.ToString() }
            };
            var currentStatus = workOrder.Status.ToString();
            if (!allowedStatus.Any(s => s.Value == currentStatus))
            {
                allowedStatus.Insert(0, new SelectListItem { Value = currentStatus, Text = currentStatus });
            }
            ViewBag.WorkOrderStatusList = allowedStatus;

            var vehicles = await _vehicleService.GetAllAsyncServiceGeneric();
            ViewBag.VehicleList = new SelectList(vehicles, "Id", "Vin");

            var workOrderDto = _mapper.Map<UpdateWorkOrderDto>(workOrder);
            var workOrderViewModel = _mapper.Map<UpdateWorkOrderViewModel>(workOrderDto);

            return View(workOrderViewModel);
        }

        // POST: WorkOrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateWorkOrderViewModel updateWorkOrderViewModel)
        {
            try
            {
                var workOrder = await _workOrderService.GetByIdAsyncServiceGeneric(updateWorkOrderViewModel.Id);
                if (workOrder == null)
                {
                    TempData["WorkOrderError"] = "WorkOrder not found";
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(updateWorkOrderViewModel);
                }
                var workOrderDto = _mapper.Map<UpdateWorkOrderDto>(updateWorkOrderViewModel);
                workOrderDto.UpdatedBy = User.Identity?.Name;
                var updatedWorkOrder = _mapper.Map(workOrderDto, workOrder);
                await _workOrderService.UpdateAsyncServiceGeneric(updatedWorkOrder);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(updateWorkOrderViewModel);
            }
        }

        // GET: WorkOrderController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var workOrder = await _workOrderService.GetByIdAsyncServiceGeneric(id, null, wo => wo.Vehicle);
            if (workOrder == null)
            {
                TempData["WorkOrderError"] = "Work Order not found";
                return RedirectToAction(nameof(Index));
            }
            var workOrderDto = _mapper.Map<DeleteWorkOrderDto>(workOrder);
            var workOrderViewModel = _mapper.Map<DeleteWorkOrderViewModel>(workOrderDto);
            return View(workOrderViewModel);
        }

        // POST: WorkOrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DeleteWorkOrderViewModel deleteWorkOrderViewModel)
        {
            try
            {
                var workOrder = await _workOrderService.GetByIdAsyncServiceGeneric(deleteWorkOrderViewModel.Id);
                if (workOrder == null)
                {
                    TempData["WorkOrderError"] = "Work Order not found";
                    return NotFound();
                }
                workOrder.IsDeleted = true;
                workOrder.DeletedAt = DateTime.Now;
                workOrder.DeletedBy = User.Identity?.Name;

                await _workOrderService.UpdateAsyncServiceGeneric(workOrder);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(deleteWorkOrderViewModel);
            }
        }
        [HttpGet]
        public async Task<IActionResult> SearchVehicles(string term)
        {
            if (string.IsNullOrWhiteSpace(term) || term.Length < 2)
                return Json(new List<object>());

            var results = await _vehicleService.GetVehicleByVin(term);
            var resultViewModel = _mapper.Map<IEnumerable<ReadVehicleViewModel>>(results);

            var response = resultViewModel.Select(v => new
            {
                id = v.Id,
                vin = v.Vin,

            });

            return Json(response);
        }
    }
}