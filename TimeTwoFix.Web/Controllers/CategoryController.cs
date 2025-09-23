using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Application.CategoryService.Dtos;
using TimeTwoFix.Application.CategoryService.Interfaces;
using TimeTwoFix.Core.Entities.ServiceManagement;
using TimeTwoFix.Web.Models.CategoryModels;

namespace TimeTwoFix.Web.Controllers
{
    [Authorize(Roles = "GeneralManager,WorkshopManager")]
    public class CategoryController : BaseController<Category
        , CreateCategoryDto, ReadCategoryDto, UpdateCategoryDto, DeleteCategoryDto,
        CreateCategoryViewModel, ReadCategoryViewModel, UpdateCategoryViewModel, DeleteCategoryViewModel>

    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, IMapper mapper) : base(categoryService, mapper)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> SearchCategories(string searchTerm)
        {
            var categories = (await _categoryService.GetAllAsyncServiceGeneric())
                .Where(x => x.IsDeleted == false);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                categories = categories
                    .Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                             || c.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewData["CurrentFilter"] = searchTerm;

            var modelsDto = _mapper.Map<List<ReadCategoryDto>>(categories);
            var viewModels = _mapper.Map<List<ReadCategoryViewModel>>(modelsDto);
            return View("Index", viewModels);
        }

    }
}