using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTwoFix.Application.CategoryService.Interfaces;
using TimeTwoFix.Application.ProvidedServicesService.Dtos;
using TimeTwoFix.Application.ProvidedServicesService.Interfaces;
using TimeTwoFix.Core.Entities.ServiceManagement;
using TimeTwoFix.Web.Models.ProvidedServiceModels;

namespace TimeTwoFix.Web.Controllers
{
    [Authorize(Roles = "GeneralManager,WorkshopManager")]
    public class ProvidedServiceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProvidedServiceService _providedServiceService;
        private readonly ICategoryService _categoryService;

        public ProvidedServiceController(IMapper mapper, IProvidedServiceService providedServiceService, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _providedServiceService = providedServiceService;
        }

        // GET: ProvidedServiceController
        public async Task<ActionResult> Index()
        {
            // Example: Fetching all provided services
            //var services = await _providedServiceService.GetAllAsyncServiceGeneric();
            var services = await _providedServiceService.GetAllWithIncludesAsyncServiceGeneric(null, s => s.Category);

            var activeServices = services.Where(s => !s.IsDeleted);
            if (activeServices == null || !activeServices.Any())
            {
                // Handle the case where no active services are found
                TempData["ServiceError"] = "No provided services found in the database.";
                return View(Enumerable.Empty<ReadProvidedServiceViewModel>());
            }

            var serviceDtos = _mapper.Map<IEnumerable<ReadProvidedServiceDto>>(activeServices);
            var servideViewModel = _mapper.Map<IEnumerable<ReadProvidedServiceViewModel>>(serviceDtos);
            return View(servideViewModel);
        }

        // GET: ProvidedServiceController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var service = await _providedServiceService.GetByIdAsyncServiceGeneric(id, null, cat => cat.Category);
            if (service == null)
            {
                return NotFound($"Provided service with ID {id} not found.");
            }
            var serviceDto = _mapper.Map<ReadProvidedServiceDto>(service);
            var serviceViewModel = _mapper.Map<ReadProvidedServiceViewModel>(serviceDto);
            return View(serviceViewModel);
        }

        // GET: ProvidedServiceController/Create
        public async Task<ActionResult> Create()
        {
            // Fetch categories for the dropdown
            var categories = await _categoryService.GetAllAsyncServiceGeneric();
            var activeCategories = categories.Where(c => !c.IsDeleted);
            ViewBag.Categories = activeCategories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View();
        }

        // POST: ProvidedServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProvidedServiceViewModel createProvidedServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch categories for the dropdown
                    var categories = await _categoryService.GetAllAsyncServiceGeneric();
                    var activeCategories = categories.Where(c => !c.IsDeleted);
                    ViewBag.Categories = activeCategories.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();
                    // Map the view model to the DTO
                    var providedServiceDto = _mapper.Map<CreateProvidedServiceDto>(createProvidedServiceViewModel);
                    var providedService = _mapper.Map<ProvidedService>(providedServiceDto);
                    await _providedServiceService.AddAsyncServiceGeneric(providedService);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the exception and show an error message
                    ModelState.AddModelError("", $"An error occurred while creating the provided service: {ex.Message}");
                    var categories = await _categoryService.GetAllAsyncServiceGeneric();
                    var activeCategories = categories.Where(c => !c.IsDeleted);
                    ViewBag.Categories = activeCategories.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();

                    return View(createProvidedServiceViewModel);
                }
            }
            // If we got this far, something failed; redisplay form with validation errors
            return View(createProvidedServiceViewModel);
        }

        // GET: ProvidedServiceController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var service = await _providedServiceService.GetByIdAsyncServiceGeneric(id);
            if (service == null)
            {
                return NotFound($"Provided service with ID {id} not found.");
            }
            var serviceDto = _mapper.Map<UpdateProvidedServiceDto>(service);
            var serviceViewModel = _mapper.Map<UpdateProvidedServiceViewModel>(serviceDto);

            // Fetch categories for the dropdown
            var categories = await _categoryService.GetAllAsyncServiceGeneric();
            var activeCategories = categories.Where(c => !c.IsDeleted);
            ViewBag.Categories = activeCategories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            return View(serviceViewModel);
        }

        // POST: ProvidedServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateProvidedServiceViewModel updateProvidedServiceViewModel)
        {
            try
            {
                var service = await _providedServiceService.GetByIdAsyncServiceGeneric(updateProvidedServiceViewModel.Id);
                if (service == null)
                {
                    TempData["ProvidedServiceError"] = "Service not found";
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(updateProvidedServiceViewModel);
                }
                var serviceDto = _mapper.Map<UpdateProvidedServiceDto>(updateProvidedServiceViewModel);
                serviceDto.UpdatedBy = User.Identity?.Name;
                var updatedService = _mapper.Map(serviceDto, service);
                await _providedServiceService.UpdateAsyncServiceGeneric(updatedService);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProvidedServiceController/Delete/5
        [Authorize(Roles = "GeneralManager")]
        public async Task<ActionResult> Delete(int id)
        {
            var service = await _providedServiceService.GetByIdAsyncServiceGeneric(id, null, c => c.Category);
            if (service == null)
            {
                return NotFound($"Provided service with ID {id} not found.");
            }
            var serviceDto = _mapper.Map<DeleteProvidedServiceDto>(service);
            var serviceViewModel = _mapper.Map<DeleteProvidedServiceViewModel>(serviceDto);
            return View(serviceViewModel);
        }

        // POST: ProvidedServiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GeneralManager")]
        public async Task<ActionResult> Delete(DeleteProvidedServiceViewModel deleteProvidedServiceViewModel)
        {
            try
            {
                var serviceToDelete = await _providedServiceService.GetByIdAsyncServiceGeneric(deleteProvidedServiceViewModel.Id);
                if (serviceToDelete == null)
                {
                    TempData["ProvidedServiceError"] = "Service not found";
                    return NotFound();
                }

                serviceToDelete.IsDeleted = true; // Mark as deleted instead of removing
                serviceToDelete.DeletedBy = User.Identity?.Name;
                serviceToDelete.DeletedAt = DateTime.Now;
                await _providedServiceService.UpdateAsyncServiceGeneric(serviceToDelete);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(deleteProvidedServiceViewModel);
            }
        }
    }
}