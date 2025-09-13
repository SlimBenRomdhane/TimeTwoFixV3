using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Application.SparePartCategoryServices.Dtos;
using TimeTwoFix.Application.SparePartCategoryServices.Interfaces;
using TimeTwoFix.Application.SparePartServices.Dtos;
using TimeTwoFix.Application.SparePartServices.Interfaces;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Web.Models.SparePartCategoryModel;
using TimeTwoFix.Web.Models.SparePartModels;

namespace TimeTwoFix.Web.Controllers
{
    public class SparePartController : BaseController<SparePart, CreateSparePartDto, ReadSparePartDto, UpdateSparePartDto, DeleteSparePartDto
        , CreateSparePartViewModel, ReadSparePartViewModel, UpdateSparePartViewModel, DeleteSparePartViewModel>
    {
        private readonly ISparePartService _sparePartService;
        private readonly ISparePartCategoryService _sparePartCategoryService;

        public SparePartController(ISparePartService sparePartService, ISparePartCategoryService sparePartCategoryService, IMapper mapper) : base(sparePartService, mapper)
        {
            _sparePartService = sparePartService;
            _sparePartCategoryService = sparePartCategoryService;
        }


        [HttpPost]
        public override async Task<IActionResult> Create(CreateSparePartViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                var dto = _mapper.Map<CreateSparePartDto>(viewModel);
                var entity = _mapper.Map<SparePart>(dto);

                if (entity is BaseEntity baseEntity)
                {
                    baseEntity.CreatedAt = DateTime.Now;
                    baseEntity.CreatedBy = User.Identity?.Name;
                }
                await _sparePartService.AddAsyncServiceGeneric(entity);
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
        public async Task<IActionResult> SearchCategories(string term)
        {
            //await _sparePartService.GetSparePartsByNameAsync(term);
            if (string.IsNullOrWhiteSpace(term) || term.Length < 2)
            {
                return Json(new List<object>());
            }
            var results = await _sparePartCategoryService.GetSparePartCategoryByNameAsync(term);
            var resultViewModels = _mapper.Map<IEnumerable<ReadSparePartCategoryDto>>(results);
            var response = resultViewModels.Select(c => new
            {
                id = c.Id,
                name = c.Name
            });
            return Json(response);
        }

        [HttpGet]
        public override async Task<IActionResult> Edit(int id)
        {
            try
            {
                var entity = await _sparePartService.GetByIdAsyncServiceGeneric(id, null, x => x.SparePartCategory);
                if (entity == null)
                {
                    TempData["ErrorMessage"] = $"{EntityName} Entity not found";
                    return NotFound();
                }
                var dto = _mapper.Map<UpdateSparePartDto>(entity);
                var viewModel = _mapper.Map<UpdateSparePartViewModel>(dto);
                viewModel.CategoryViewModel.Name = dto.CategoryDto.Name;
                return View(viewModel);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occured while loading data";
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public override async Task<IActionResult> Edit(int id, UpdateSparePartViewModel viewModel)
        {
            ModelState.Remove(nameof(viewModel.CategoryViewModel));

            var category = await _sparePartCategoryService.GetByIdAsyncServiceGeneric(viewModel.SparePartCategoryId);
            var catDto = _mapper.Map<ReadSparePartCategoryDto>(category);
            viewModel.CategoryViewModel = _mapper.Map<ReadSparePartCategoryViewModel>(catDto);
            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                TempData["ErrorMessage"] = "Validation failed: " + string.Join(" | ", errors);
                return View(viewModel);
            }
            try
            {
                var existingEntity = await _sparePartService.GetByIdAsyncServiceGeneric(id/*, includes*/);
                if (existingEntity == null)
                {
                    TempData["ErrorMessage"] = $"{EntityName} Entity not found";
                    return NotFound();
                }
                var dto = _mapper.Map<UpdateSparePartDto>(viewModel);
                var updatedEntity = _mapper.Map(dto, existingEntity);

                if (updatedEntity is BaseEntity baseEntity)
                {
                    baseEntity.UpdatedAt = DateTime.Now;
                    baseEntity.UpdatedBy = User.Identity?.Name;
                }
                await _sparePartService.UpdateAsyncServiceGeneric(updatedEntity);
                TempData["SuccessMessage"] = $"{EntityName} updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occured while updating the entity";
                return View(viewModel);
            }
        }

        [HttpGet]
        [Route("SparePart/SearchSpareParts")]
        public async Task<IActionResult> SearchSpareParts(string term)
        {
            if (string.IsNullOrWhiteSpace(term) || term.Length < 2)
                return Json(new List<object>());

            var results = await _sparePartService.GetSparePartsByNameAsync(term);
            var viewModels = _mapper.Map<IEnumerable<ReadSparePartViewModel>>(results);

            var response = viewModels.Select(sp => new
            {
                id = sp.Id,
                name = sp.Name
            });

            return Json(response);
        }

    }


}
