using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Common.Constants;
using TimeTwoFix.Core.Entities.WorkOrderManagement;
using TimeTwoFix.Web.Models.InterventionModels;
using TimeTwoFix.Web.Models.VehicleModels;
using TimeTwoFix.Web.Models.WorkOrderModels;
using TimeTwoFix.Application.VehicleServices.Interfaces;
using TimeTwoFix.Application.WorkOrderService.Dtos;
using TimeTwoFix.Application.WorkOrderService.Interfaces;

namespace TimeTwoFix.Web.Controllers
{
    [Authorize(Roles = RoleNames.Combined.AllManagers)]
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
            .OrderByDescending(wo =>wo.PaymentDate)
            );
            if (workOrders == null || !workOrders.Any())
            {
                TempData["ErrorMessage"] = "No WorkOrder found in the database";
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
        [HttpGet]
        public async Task<ActionResult> FilterByPaymentStatus(string paid)
        {
            var workOrders = await _workOrderService.GetAllWithIncludesAsyncServiceGeneric(includeBuilder: query => query
                .Include(wo => wo.Vehicle)
                .Include(wo => wo.Interventions)
                    .ThenInclude(inter => inter.InterventionSpareParts)
                    .ThenInclude(isp => isp.SparePart)
            );

            if (workOrders == null || !workOrders.Any())
            {
                TempData["ErrorMessage"] = "No WorkOrder found in the database";
                return View("Index", Enumerable.Empty<ReadWorkOrderViewModel>());
            }

            workOrders = workOrders.Where(wo => !wo.IsDeleted);

            // Apply payment filter
            if (!string.IsNullOrEmpty(paid))
            {
                if (paid == "true")
                    workOrders = workOrders.Where(wo => wo.Paid == true);
                else if (paid == "false")
                    workOrders = workOrders.Where(wo => wo.Paid == false);
            }

            foreach (var workOrder in workOrders)
            {
                workOrder.RecalculateLaborCost();
                workOrder.UpdateStatus2();
            }

            await _workOrderService.SaveChangesServiceGeneric();

            var workOrderDtos = _mapper.Map<IEnumerable<ReadWorkOrderDto>>(workOrders);
            var workOrderViewModels = _mapper.Map<IEnumerable<ReadWorkOrderViewModel>>(workOrderDtos);

            ViewBag.PaidFilter = paid;
            return View("Index", workOrderViewModels);
        }
        // GET: WorkOrderController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var workOrder = await _workOrderService.GetByIdAsyncServiceGeneric(id, includeBuilder: query => query
            .Include(wo => wo.Vehicle)
            .Include(wo => wo.Interventions)
                .ThenInclude(inter => inter.InterventionSpareParts)
                    .ThenInclude(isp => isp.SparePart)
            .Include(wo => wo.Interventions)
                .ThenInclude(inter => inter.Service)
            .Include(wo => wo.Interventions)
                .ThenInclude(inter => inter.Mechanic));
            if (workOrder == null)
            {
                TempData["ErrorMessage"] = "WorkOrder not found";
                return RedirectToAction(nameof(Index));
            }
            foreach (var intervention in workOrder.Interventions)
            {
                var spareParts = intervention.InterventionSpareParts
                    .Where(isp => !isp.IsDeleted)
                    .Select(sp => new InterventionSparePartDisplayViewModel
                    {
                        SparePartName = sp.SparePart.Name,
                        Quantity = sp.Quantity,
                        UnitPrice = sp.SparePart?.UnitPrice ?? 0
                    }).ToList();

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
            var lastWorkOrder = (await _workOrderService.GetAllAsyncServiceGeneric())
                .Where(wo => wo.VehicleId == createWorkOrderViewModel.VehicleId)
                .OrderByDescending(wo => wo.CreatedAt)
                .FirstOrDefault();
            if (lastWorkOrder == null)
            {
                TempData["SuccessMessage"] = "This is the first recorded work order for this vehicle.";
            }
            if (createWorkOrderViewModel.Mileage <= lastWorkOrder?.Mileage)
            {
                TempData["ErrorMessage"] = $"Mileage must be greater than the last recorded value ({lastWorkOrder.Mileage} km).";
                return View("CreateById", createWorkOrderViewModel);

            }
            var vehicles = await _vehicleService.GetAllAsyncServiceGeneric();
            ViewBag.VehicleList = new SelectList(vehicles, "Id", "Vin");
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid data provided";
                return View(createWorkOrderViewModel);
            }
            try
            {
                var workOrderDto = _mapper.Map<CreateWorkOrderDto>(createWorkOrderViewModel);
                workOrderDto.CreatedBy = User.Identity?.Name;
                var workOrder = _mapper.Map<WorkOrder>(workOrderDto);
                await _workOrderService.AddAsyncServiceGeneric(workOrder);
                TempData["SuccessMessage"] = "Work Order created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while creating the Work Order: {ex.Message}";
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
                TempData["SuccessMessage"] = "Work Order created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while creating the Work Order: {ex.Message}";
                return View(createWorkOrderViewModel);
            }
        }

        // GET: WorkOrderController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var workOrder = await _workOrderService.GetByIdAsyncServiceGeneric(id);
            if (workOrder == null)
            {
                TempData["ErrorMessage"] = "Work Order not found";
                return RedirectToAction(nameof(Index));
            }
            if (workOrder.Paid == true)
            {
                TempData["ErrorMessage"] = "Paid work orders cannot be modified.";
                return RedirectToAction("Index");
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
                    TempData["ErrorMessage"] = "WorkOrder not found";
                    return NotFound();
                }
                if (workOrder.Paid == true && !User.IsInRole(RoleNames.GeneralManager))
                {
                    TempData["ErrorMessage"] = "Paid work orders cannot be modified.";
                    return RedirectToAction("Index");
                }
                if (!ModelState.IsValid)
                {
                    return View(updateWorkOrderViewModel);
                }
                // 🔎 Role‑based restriction
                if (User.IsInRole(RoleNames.FrontDeskAssistant))
                {
                    // Check if fields other than Paid/PaymentDate are being changed
                    bool otherFieldsChanged =

                        updateWorkOrderViewModel.Mileage != workOrder.Mileage ||
                        updateWorkOrderViewModel.StartDate != workOrder.StartDate ||
                        //updateWorkOrderViewModel.StartTime != workOrder.StartTime ||
                        updateWorkOrderViewModel.EndDate != workOrder.EndDate ||
                        // updateWorkOrderViewModel.EndTime != workOrder.EndTime ||
                        updateWorkOrderViewModel.TolalLaborCost != workOrder.TolalLaborCost ||
                        updateWorkOrderViewModel.Status != workOrder.Status ||
                        updateWorkOrderViewModel.Notes != workOrder.Notes;

                    if (otherFieldsChanged)
                    {
                        TempData["ErrorMessage"] = "Front desk assistants can only update payment fields.";
                        return RedirectToAction("Index");
                    }
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
        public async Task<IActionResult> ExportToPdf(int id)
        {
            //var includes = EntityIncludeHelper.GetIncludes<WorkOrder>();
            var workOrder = await _workOrderService.GetByIdAsyncServiceGeneric(id, includeBuilder: query => query
            .Include(wo => wo.Vehicle)
                .ThenInclude(v => v.Client)
            .Include(wo => wo.Interventions)
                .ThenInclude(inter => inter.InterventionSpareParts)
                    .ThenInclude(isp => isp.SparePart)
            .Include(wo => wo.Interventions)
                .ThenInclude(inter => inter.Service)
            .Include(wo => wo.Interventions)
                .ThenInclude(inter => inter.Mechanic));

            var exportModel = new WorkOrderExportViewModel
            {
                WorkOrderId = workOrder.Id,
                // Pull customer info from Vehicle
                CustomerName = workOrder.Vehicle?.Client?.FirstName,
                CustomerLastName = workOrder.Vehicle?.Client?.LastName,
                CustomerPhone = workOrder.Vehicle?.Client?.PhoneNumber,
                CustomerEmail = workOrder.Vehicle?.Client?.Email,

                LicensePlate = workOrder.Vehicle.LicensePlate,
                Brand = workOrder.Vehicle.Brand,
                Model = workOrder.Vehicle.Model,
                Vin = workOrder.Vehicle.Vin,
                Mileage = workOrder.Mileage,
                LaborCost = workOrder.TolalLaborCost,
                Notes = workOrder.Notes,
                Interventions = workOrder.Interventions.Select(iv => new ExportInterventionViewModel
                {
                    ServiceName = iv.Service.Name,
                    ServiceCost = iv.InterventionPrice,
                    SpareParts = iv.InterventionSpareParts.Select(sp => new ExportSparePartViewModel
                    {
                        Name = sp.SparePart.Name,
                        Quantity = sp.Quantity,
                        UnitPrice = sp.SparePart.UnitPrice
                    }).ToList()
                }).ToList()
            };
            //var workOrderDto = _mapper.Map<ReadWorkOrderDto>(workOrder);
            var workOrderViewModel = _mapper.Map<ReadWorkOrderViewModel>(exportModel);
            var pdfBytes = WorkOrderPdfGenerator.Generate(workOrderViewModel);
            return File(pdfBytes, "application/pdf", $"WorkOrder_{id}.pdf");
        }
    }
}
