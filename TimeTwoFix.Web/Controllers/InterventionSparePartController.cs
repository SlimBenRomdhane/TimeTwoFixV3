using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTwoFix.Core.Common.Constants;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Infrastructure.Persistence.Includes;
using TimeTwoFix.Web.Models.InterventionSparePartModel;
using TimeTwoFix.Application.InterventionService.Interfaces;
using TimeTwoFix.Application.InterventionSparePartServices.Dtos;
using TimeTwoFix.Application.InterventionSparePartServices.Interfaces;
using TimeTwoFix.Application.SparePartServices.Interfaces;
using TimeTwoFix.Application.WorkOrderService.Interfaces;
using TimeTwoFix.Core.Common;

namespace TimeTwoFix.Web.Controllers
{
    [Authorize(Roles = $"{RoleNames.WareHouseManager},{RoleNames.GeneralManager}")]
    public class InterventionSparePartController : BaseController<InterventionSparePart
        , CreateInterventionSparePartDto
        , ReadInterventionSparePartDto
        , UpdateInterventionSparePartDto
        , DeleteInterventionSparePartDto
        , CreateInterventionSparePartViewModel
        , ReadInterventionSparePartViewModel
        , UpdateInterventionSparePartViewModel
        , DeleteInterventionSparePartViewModel>
    {
        private readonly IInterventionSparePartService _interventionSparePartService;
        private readonly ISparePartService _sparePartService;
        private readonly IInterventionService _interventionService;
        private readonly IWorkOrderService _workOrderService;

        public InterventionSparePartController(IInterventionSparePartService baseService
            , ISparePartService sparePartService
            , IWorkOrderService workOrderService
            , IInterventionService interventionService
            , IMapper mapper) : base(baseService, mapper)
        {
            _interventionSparePartService = baseService;
            _sparePartService = sparePartService;
            _workOrderService = workOrderService;
            _interventionService = interventionService;
        }

        public override async Task<IActionResult> Index()
        {
            try
            {
                var includes = EntityIncludeHelper.GetIncludes<InterventionSparePart>(); // or manually define includes
                var entities = await _interventionSparePartService.GetAllWithIncludesAsyncServiceGeneric(null, includes);
                var activeEntities = entities.Where(isp => isp.IsDeleted == false);

                if (!activeEntities.Any())
                {
                    TempData["ErrorMessage"] = "No spare parts found for interventions.";
                    return View(Enumerable.Empty<ReadInterventionSparePartViewModel>());
                }

                var dtos = _mapper.Map<IEnumerable<ReadInterventionSparePartDto>>(activeEntities);
                var viewModels = _mapper.Map<IEnumerable<ReadInterventionSparePartViewModel>>(dtos);

                TempData["SuccessMessage"] = $"Loaded {viewModels.Count()} items.";
                return View(viewModels);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while loading intervention spare parts.";
                return View(Enumerable.Empty<ReadInterventionSparePartViewModel>());
            }
        }

        public override async Task<ActionResult> Create()
        {
            var interventions = await _interventionService.GetAllAsyncServiceGeneric();
            var res = interventions.Where(c => c.IsDeleted == false && c.Status != "Completed");
            ViewBag.ActiveIntervention = new SelectList(res.Select(c => new
            {
                c.Id,
                Display = $"WO#{c.WorkOrderId} - Intervention#{c.Id}"

            }), "Id", "Display");
            return await base.Create();
        }

        [HttpPost]
        public override async Task<IActionResult> Create(CreateInterventionSparePartViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var spareParts = await _sparePartService.GetByIdAsyncServiceGeneric(viewModel.SparePartId);
            if (spareParts == null)
            {
                TempData["ErrorMessage"] = "Spare part not found";
                return View(viewModel);
            }

            if (viewModel.Quantity <= spareParts.QuantityInStock)
            {
                try
                {
                    var dto = _mapper.Map<CreateInterventionSparePartDto>(viewModel);
                    var entity = _mapper.Map<InterventionSparePart>(dto);

                    if (entity is BaseEntity baseEntity)
                    {
                        baseEntity.CreatedAt = DateTime.Now;
                        baseEntity.CreatedBy = User.Identity?.Name;
                    }

                    await _interventionSparePartService.AddAsyncServiceGeneric(entity);
                    spareParts.DecreaseStock(entity);
                    spareParts.UpdatedAt = DateTime.Now;
                    spareParts.UpdatedBy = User.Identity?.Name;
                    await _sparePartService.UpdateAsyncServiceGeneric(spareParts);

                    TempData["SuccessMessage"] = $"{EntityName} Entity created successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "An error occured while creating the entity";
                    return View(viewModel);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Insufficient stock for the requested quantity.";
                return View(viewModel);
            }
        }


        [HttpGet]
        public async Task<ActionResult> CreateByInterventionId(int interventionId)
        {
            var intervention = await _interventionService.GetByIdAsyncServiceGeneric(interventionId);
            if (intervention == null || intervention.IsDeleted)
            {
                TempData["ErrorMessage"] = "Intervention not found.";
                return RedirectToAction("Index", "Intervention");
            }
            if (intervention.Status == "Completed" && !User.IsInRole(RoleNames.GeneralManager))
            {
                TempData["ErrorMessage"] = "Cannot add spare parts to an intervention that is  completed.";
                return RedirectToAction("Details", "Intervention", new { id = interventionId });
            }

            return await base.Create();

        }
    }
}
