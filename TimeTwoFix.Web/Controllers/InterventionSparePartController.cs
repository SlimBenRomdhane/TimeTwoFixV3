using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Application.InterventionSparePartServices.Dtos;
using TimeTwoFix.Application.InterventionSparePartServices.Interfaces;
using TimeTwoFix.Application.SparePartServices.Interfaces;
using TimeTwoFix.Application.WorkOrderService.Interfaces;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Web.Models.InterventionSparePartModel;

namespace TimeTwoFix.Web.Controllers
{
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
        private readonly IWorkOrderService _workOrderService;

        public InterventionSparePartController(IInterventionSparePartService baseService
            , ISparePartService sparePartService
            , IWorkOrderService workOrderService
            , IMapper mapper) : base(baseService, mapper)
        {
            _interventionSparePartService = baseService;
            _sparePartService = sparePartService;
            _workOrderService = workOrderService;
        }

        public override async Task<ActionResult> Create()
        {
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
    }
}