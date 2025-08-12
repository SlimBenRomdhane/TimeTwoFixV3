using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Application.CategoryService.Dtos;
using TimeTwoFix.Application.CategoryService.Interfaces;
using TimeTwoFix.Core.Entities.ServiceManagement;
using TimeTwoFix.Core.Interfaces;
using TimeTwoFix.Web.Models.CategoryModels;

namespace TimeTwoFix.Web.Controllers
{
    [Authorize(Roles = "GeneralManager,WorkshopManager")]
    public class CategoryController : Controller

    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;


        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper, ICategoryService appointmentServices)
        {
            //_unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryService = appointmentServices;

        }
        // GET: CategoryController
        public async Task<ActionResult> Index()
        {
            try
            {
                var categories = await _categoryService.GetAllAsyncServiceGeneric();
                var activeCategories = categories.Where(c => !c.IsDeleted);
                if (categories == null || !activeCategories.Any())
                {
                    // Handle the case where no active categories are found
                    TempData["CategoryError"] = "No categories found in the database";
                    return View(Enumerable.Empty<ReadCategoryViewModel>());
                }
                var categoryDtos = _mapper.Map<IEnumerable<ReadCategoryDto>>(activeCategories);
                var categoryViewModel = _mapper.Map<IEnumerable<ReadCategoryViewModel>>(categoryDtos);
                return View(categoryViewModel);

            }
            catch (Exception)
            {
                TempData["CategoryError"] = "An error occurred while retrieving the category";
                return View(Enumerable.Empty<ReadCategoryViewModel>());
            }

        }
        [HttpPost]
        public async Task<ActionResult> Index(string categoryName)
        {
            try
            {
                var categories = await _categoryService.GetCategoryByName(categoryName);
                if (categories == null || !categories.Any())
                {
                    TempData["CategoryError"] = "No categories found with the specified name";
                    return View(Enumerable.Empty<ReadCategoryViewModel>());
                }
                var categoryViewModel = _mapper.Map<IEnumerable<ReadCategoryViewModel>>(categories);
                return View(categoryViewModel);


            }
            catch (Exception)
            {
                TempData["CategoryError"] = "An error occurred while retrieving the category";
                return View(Enumerable.Empty<ReadCategoryViewModel>());
            }

        }

        // GET: CategoryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsyncServiceGeneric(id);
                if (category == null)
                {
                    TempData["CategoryError"] = "Category not found";
                    return NotFound();
                }
                var categoryDto = _mapper.Map<ReadCategoryDto>(category);
                var categoryViewModel = _mapper.Map<ReadCategoryViewModel>(categoryDto);
                return View(categoryViewModel);
            }
            catch (Exception)
            {
                TempData["CategoryError"] = "An error occurred while retrieving the category";
                return View("Error");
            }

        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCategoryViewModel createCategoryViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var categoryDto = _mapper.Map<CreateCategoryDto>(createCategoryViewModel);
                    categoryDto.CreatedBy = User.Identity?.Name;
                    var category = _mapper.Map<Category>(categoryDto);
                    var result = await _categoryService.AddAsyncServiceGeneric(category);
                    if (result != null)
                    {
                        TempData["CategorySuccess"] = "Category created successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["CategoryError"] = "Failed to create category";
                    }
                }
                return View(createCategoryViewModel);
            }
            catch (Exception)
            {
                TempData["CategoryError"] = "An error occurred while creating the category";
                return View(createCategoryViewModel);
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsyncServiceGeneric(id);
            if (category == null)
            {
                TempData["CategoryError"] = "Category not found";
                return NotFound();
            }
            var categoryDto = _mapper.Map<UpdateCategoryDto>(category);
            var categoryViewModel = _mapper.Map<UpdateCategoryViewModel>(categoryDto);

            return View(categoryViewModel);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateCategoryViewModel updateCategoryViewModel)
        {
            try
            {
                var category = await _categoryService.GetByIdAsyncServiceGeneric(updateCategoryViewModel.Id);
                if (category == null)
                {
                    TempData["CategoryError"] = "Category not found";
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(updateCategoryViewModel);
                }
                var categoryDto = _mapper.Map<UpdateCategoryDto>(updateCategoryViewModel);
                categoryDto.UpdatedBy = User.Identity?.Name;
                var updatedCategory = _mapper.Map(categoryDto, category);
                await _categoryService.UpdateAsyncServiceGeneric(updatedCategory);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        [Authorize(Roles = "GeneralManager")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsyncServiceGeneric(id);
                if (category == null || category.IsDeleted == true)
                {
                    TempData["CategoryError"] = "Category not found";
                    return View();
                }
                var categoryDto = _mapper.Map<DeleteCategoryDto>(category);
                var categoryViewModel = _mapper.Map<DeleteCategoryViewModel>(categoryDto);
                return View(categoryViewModel);
            }
            catch (Exception ex)
            {
                TempData["CategoryError"] = "An error occurred while retrieving the category for deletion " + ex.Message;

                return View();
            }

        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GeneralManager")]
        public async Task<ActionResult> Delete(DeleteCategoryViewModel deleteCategoryViewModel)
        {
            try
            {
                var category = await _categoryService.GetByIdAsyncServiceGeneric(deleteCategoryViewModel.Id);

                if (category == null || category.IsDeleted == true)
                {
                    TempData["CategoryError"] = "Category not found";
                    return NotFound();
                }

                category.IsDeleted = true; // Mark as deleted
                category.DeletedBy = User.Identity?.Name;
                category.DeletedAt = DateTime.Now; // Set deletion timestamp

                await _categoryService.UpdateAsyncServiceGeneric(category);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(deleteCategoryViewModel);
            }
        }
    }
}
