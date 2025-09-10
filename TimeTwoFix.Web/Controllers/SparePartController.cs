using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTwoFix.Application.SparePartCategoryServices.Dtos;
using TimeTwoFix.Application.SparePartCategoryServices.Interfaces;
using TimeTwoFix.Application.SparePartServices.Dtos;
using TimeTwoFix.Application.SparePartServices.Interfaces;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.SparePartManagement;
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

        [HttpGet]
        public override async Task<ActionResult> Create()
        {
            //var sparePartCategories = (await _sparePartCategoryService.GetAllAsyncServiceGeneric()).Where(c => !c.IsDeleted);
            //ViewBag.Categories = new SelectList(sparePartCategories, "Id", "Name");
            return View();

        }


        [HttpPost]
        public override async Task<IActionResult> Create(CreateSparePartViewModel viewModel)
        {

            var sparePartCategories = (await _sparePartCategoryService.GetAllAsyncServiceGeneric()).Where(c => !c.IsDeleted);
            ViewBag.Categories = new SelectList(sparePartCategories, "Id", "Name");
            //return await base.Create(viewModel);

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
                await _baseService.AddAsyncServiceGeneric(entity);
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
            if (string.IsNullOrWhiteSpace(term) || term.Length < 2)
            {
                return Json(new List<object>());
            }
            var results = await _sparePartCategoryService.GetSparePartCategoryByNameAsync(term);
            var resultViewModels = _mapper.Map<IEnumerable<ReadSparePartCategoryDto>>(results);
            return Json(resultViewModels.Select(c => new
            {
                id = c.Id,
                name = c.Name,
                description = c.Description
            }));
        }

    }
}