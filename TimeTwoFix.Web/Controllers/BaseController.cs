using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Common.Constants;
using TimeTwoFix.Core.Common.Exceptions;

namespace TimeTwoFix.Web.Controllers
{
    public abstract class BaseController<TEntity, TCreateDto, TReadDto, TUpdateDto, TDeleteDto,
        TCreateViewModel, TReadViewModel, TUpdateViewModel, TDeleteViewModel> : Controller
        where TEntity : class
        where TCreateDto : class
        where TReadDto : class
        where TUpdateDto : class
        where TDeleteDto : class
        where TCreateViewModel : class
        where TReadViewModel : class
        where TUpdateViewModel : class
        where TDeleteViewModel : class
    {
        protected readonly IBaseService<TEntity> _baseService;
        protected readonly IMapper _mapper;
        protected string EntityName => typeof(TEntity).Name;

        public BaseController(IBaseService<TEntity> baseService, IMapper mapper)
        {
            _baseService = baseService;
            _mapper = mapper;
        }

        public virtual async Task<IActionResult> Index()
        {
            var entities = await _baseService.GetAllAsyncServiceGeneric();
            var activeEntities = entities.Where(e => !(e is BaseEntity be) || !be.IsDeleted);
            if (entities == null || !activeEntities.Any())
            {
                TempData["ErrorMessage"] = $"No {EntityName.ToLower()} found";
                return View(Enumerable.Empty<TReadViewModel>());
            }
            var dtos = _mapper.Map<IEnumerable<TReadDto>>(activeEntities);
            var viewModels = _mapper.Map<IEnumerable<TReadViewModel>>(dtos);
            TempData["SuccessMessage"] = $"Successfully loaded {viewModels.Count()} {EntityName.ToLower()}{(viewModels.Count() == 1 ? "" : "s")}.";
            return View(viewModels);
        }

        public virtual async Task<IActionResult> Details(int id)
        {
            var entity = await _baseService.GetByIdAsyncServiceGeneric(id);
            if (entity == null)
            {
                TempData["ErrorMessage"] = $"{EntityName} not found";
                return NotFound();
            }
            var dto = _mapper.Map<TReadDto>(entity);
            var viewModel = _mapper.Map<TReadViewModel>(dto);
            TempData["SuccessMessage"] = $"{EntityName} details loaded successfully.";
            return View(viewModel);
        }

        // Base async method intentionally lacks 'await' to allow child controllers to override with asynchronous logic.
        // This method defines the async contract for extensibility — child implementations may perform async operations.
        // Suppressing CS1998 to avoid misleading compiler warnings; this is a deliberate design choice.
#pragma warning disable CS1998

        public virtual async Task<ActionResult> Create()
        {
            return View();
        }

#pragma warning restore CS1998// Async method lacks 'await' operators and will run synchronously

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dto = _mapper.Map<TCreateDto>(viewModel);
            var entity = _mapper.Map<TEntity>(dto);

            if (entity is BaseEntity baseEntity)
            {
                baseEntity.CreatedAt = DateTime.Now;
                baseEntity.CreatedBy = User.Identity?.Name;
            }
            await _baseService.AddAsyncServiceGeneric(entity);
            TempData["SuccessMessage"] = $"{EntityName} created successfully";
            return RedirectToAction(nameof(Index));
        }

        public virtual async Task<IActionResult> Edit(int id)
        {
            var entity = await _baseService.GetByIdAsyncServiceGeneric(id);
            if (entity == null)
            {
                TempData["ErrorMessage"] = $"{EntityName} Entity not found";
                return NotFound();
            }
            var dto = _mapper.Map<TUpdateDto>(entity);
            var viewModel = _mapper.Map<TUpdateViewModel>(dto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(int id, TUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                TempData["ErrorMessage"] = "Validation failed: " + string.Join(" | ", errors);
                return View(viewModel);
            }

            var existingEntity = await _baseService.GetByIdAsyncServiceGeneric(id);
            if (existingEntity == null)
            {
                TempData["ErrorMessage"] = $"{EntityName} not found";
                return NotFound();
            }
            var dto = _mapper.Map<TUpdateDto>(viewModel);
            var updatedEntity = _mapper.Map(dto, existingEntity);

            if (updatedEntity is BaseEntity baseEntity)
            {
                baseEntity.UpdatedAt = DateTime.Now;
                baseEntity.UpdatedBy = User.Identity?.Name;
            }
            await _baseService.UpdateAsyncServiceGeneric(updatedEntity);
            TempData["SuccessMessage"] = $"{EntityName} updated successfully";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var entity = await _baseService.GetByIdAsyncServiceGeneric(id);
            if (entity == null)
            {
                TempData["ErrorMessage"] = $"{EntityName} not found";
                return NotFound();
            }
            var dto = _mapper.Map<TDeleteDto>(entity);
            var viewModel = _mapper.Map<TDeleteViewModel>(dto);
            TempData["SuccessMessage"] = $"{EntityName} details loaded for deletion.";
            return View(viewModel);
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Delete(int id, TDeleteViewModel viewModel)
        {
            var entity = await _baseService.GetByIdAsyncServiceGeneric(id);
            if (entity == null)
            {
                TempData["ErrorMessage"] = $"{EntityName} not found";
                return NotFound();
            }
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.IsDeleted = true;
                baseEntity.DeletedAt = DateTime.Now;
                baseEntity.DeletedBy = User.Identity?.Name;
                await _baseService.UpdateAsyncServiceGeneric(entity);
                TempData["SuccessMessage"] = $"{EntityName} deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _baseService.DeleteAsyncServiceGeneric(id);
                TempData["SuccessMessage"] = "Entity deleted successfully";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
