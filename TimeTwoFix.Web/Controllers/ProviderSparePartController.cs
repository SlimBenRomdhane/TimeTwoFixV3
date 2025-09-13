using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;
using TimeTwoFix.Application.ProviderSparePartServices.Dtos;
using TimeTwoFix.Application.ProviderSparePartServices.Interfaces;
using TimeTwoFix.Application.SparePartServices.Interfaces;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces;
using TimeTwoFix.Web.Models.ProviderSparePartModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TimeTwoFix.Web.Controllers
{
    public class ProviderSparePartController : BaseController<ProviderSparePart
        , CreateProviderSparePartDto, ReadProviderSparePartDto, UpdateProviderSparePartDto, DeleteProviderSparePartDto
        , CreateProviderSparePartViewModel, ReadProviderSparePartViewModel, UpdateProviderSparePartViewModel, DeleteProviderSparePartViewModel>
    {
        private readonly IProviderSparePartService _providerSparePartService;
        private readonly ISparePartService _sparePartService;
        private readonly IUnitOfWork _unitOfWork;
        public ProviderSparePartController(IProviderSparePartService providerSparePartService
            , ISparePartService sparePartService
            , IMapper mapper
            , IUnitOfWork unitOfWork) : base(providerSparePartService, mapper)
        {
            _providerSparePartService = providerSparePartService;
            _sparePartService = sparePartService;
            _unitOfWork = unitOfWork;
        }


        public override Task<ActionResult> Create()
        {
            return base.Create();
        }

        [HttpPost]
        public override async Task<IActionResult> Create(CreateProviderSparePartViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var spareParts = await _sparePartService.GetByIdAsyncServiceGeneric(viewModel.SparePartId, null, sp => sp.ProviderSpareParts);
            if (spareParts == null)
            {
                TempData["ErrorMessage"] = "Spare part not found";
                return View(viewModel);
            }
            try
            {
                var dto = _mapper.Map<CreateProviderSparePartDto>(viewModel);
                var entity = _mapper.Map<ProviderSparePart>(dto);

                if (entity is BaseEntity baseEntity)
                {
                    baseEntity.CreatedAt = DateTime.Now;
                    baseEntity.CreatedBy = User.Identity?.Name;
                }
                await _providerSparePartService.AddAsyncServiceGeneric(entity);
                spareParts.IncreaseStock(entity);
                spareParts.CalculateUnitPrice(entity);
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

        [HttpGet]
        public IActionResult CreateBulk(int providerId)
        {
            var model = new BulkProviderSparePartViewModel
            {
                ProviderId = providerId,
                SpareParts = new List<CreateProviderSparePartViewModel>()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBulk(BulkProviderSparePartViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);
            IDbContextTransaction transaction = null;
            try
            {
                using (transaction = await _unitOfWork.BeginTransactionAsync())
                {
                    foreach (var item in model.SpareParts)
                    {
                        // Fetch the linked SparePart entity
                        var sparePart = await _sparePartService.GetByIdAsyncServiceGeneric(
                            item.SparePartId,
                             null,
                             sp => sp.ProviderSpareParts
                        );

                        if (sparePart == null)
                        {
                            TempData["ErrorMessage"] = $"Spare part with ID {item.SparePartId} not found.";
                            continue;
                        }

                        // Map to DTO and then to entity
                        var dto = _mapper.Map<CreateProviderSparePartDto>(item);
                        var entity = _mapper.Map<ProviderSparePart>(dto);
                        entity.ProviderId = model.ProviderId;

                        if (entity is BaseEntity baseEntity)
                        {
                            baseEntity.CreatedAt = DateTime.Now;
                            baseEntity.CreatedBy = User.Identity?.Name;
                        }

                        // Persist the ProviderSparePart
                        await _providerSparePartService.AddAsyncServiceGeneric(entity);

                        // Update SparePart stock and pricing
                        sparePart.IncreaseStock(entity);
                        sparePart.CalculateUnitPrice(entity);
                        sparePart.UpdatedAt = DateTime.Now;
                        sparePart.UpdatedBy = User.Identity?.Name;

                        await _sparePartService.UpdateAsyncServiceGeneric(sparePart);
                    }

                    await _providerSparePartService.SaveChangesServiceGeneric();
                    await _sparePartService.SaveChangesServiceGeneric();
                    await transaction.CommitAsync();

                    TempData["SuccessMessage"] = $"Successfully received {model.SpareParts.Count} spare parts.";
                    return RedirectToAction("Index", "Provider");
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                TempData["ErrorMessage"] = "An error occurred while saving spare parts.";
                return View(model);

            }
        }
    }

}
