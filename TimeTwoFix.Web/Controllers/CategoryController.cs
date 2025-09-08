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
    public class CategoryController : BaseController<Category, CreateCategoryDto, ReadCategoryDto, UpdateCategoryDto, DeleteCategoryDto,
        CreateCategoryViewModel, ReadCategoryViewModel, UpdateCategoryViewModel, DeleteCategoryViewModel>

    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, IMapper mapper) : base(categoryService, mapper)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Roles = "GeneralManager")]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }

        [HttpPost]
        [Authorize(Roles = "GeneralManager")]
        public override async Task<IActionResult> Delete(int id, DeleteCategoryViewModel viewModel)
        {
            return await base.Delete(id, viewModel);
        }
    }
}