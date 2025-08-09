using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTwoFix.Application.LiftingBridgeServices.Dtos;
using TimeTwoFix.Application.LiftingBridgeServices.Interfaces;
using TimeTwoFix.Core.Entities.BridgeManagement;
using TimeTwoFix.Web.Models.LiftingBridgeModels;

namespace TimeTwoFix.Web.Controllers
{
    public class LiftingBridgeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILiftingBridgeServices _liftingBridgeServices;

        public LiftingBridgeController(IMapper mapper, ILiftingBridgeServices liftingBridgeServices)
        {
            _mapper = mapper;
            _liftingBridgeServices = liftingBridgeServices;
        }
        // GET: LiftingBridgeController
        public async Task<ActionResult> Index()
        {

            var liftingBridges = await _liftingBridgeServices.GetAllAsyncServiceGeneric();
            var notDeleted = liftingBridges.Where(b => !b.IsDeleted);
            if (notDeleted == null || !notDeleted.Any())
            {
                return View(new List<ReadLiftingBridgeViewModel>());
            }
            var liftingBridgeDtos = _mapper.Map<IEnumerable<ReadLiftingBridgeDto>>(notDeleted);

            var liftingBridgeViewModels = _mapper.Map<IEnumerable<ReadLiftingBridgeViewModel>>(liftingBridgeDtos);
            return View(liftingBridgeViewModels);

        }

        // GET: LiftingBridgeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var liftingBridge = await _liftingBridgeServices.GetByIdAsyncServiceGeneric(id);
            if (liftingBridge == null)
            {
                return NotFound();
            }
            var liftingBridgeDto = _mapper.Map<ReadLiftingBridgeDto>(liftingBridge);
            var liftingBridgeViewModel = _mapper.Map<ReadLiftingBridgeViewModel>(liftingBridgeDto);

            return View(liftingBridgeViewModel);
        }

        // GET: LiftingBridgeController/Create
        public ActionResult Create()
        {
            ViewBag.BridgeStatus = new SelectList(new[]
            {
                new { Value = "Idle", Text = "Idle" },
                new { Value = "Occupied", Text = "Occupied" },
                new { Value = "Out Of Service", Text = "Out Of Service" },
            }, "Value", "Text");
            return View();
        }

        // POST: LiftingBridgeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLiftingBridgeViewModel createLiftingBridgeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createLiftingBridgeDto = _mapper.Map<CreateLiftingBridgeDto>(createLiftingBridgeViewModel);
                    var bridgeToCreate = _mapper.Map<LiftingBridge>(createLiftingBridgeDto);
                    bridgeToCreate.CreatedAt = DateTime.Now;
                    bridgeToCreate.CreatedBy = User.Identity?.Name;
                    await _liftingBridgeServices.AddAsyncServiceGeneric(bridgeToCreate);
                    return RedirectToAction(nameof(Index));
                }
                return View(createLiftingBridgeViewModel);
            }
            catch
            {
                return View();
            }
        }

        // GET: LiftingBridgeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.BridgeStatus = new SelectList(new[]
            {
                new { Value = "Idle", Text = "Idle" },
                new { Value = "Occupied", Text = "Occupied" },
                new { Value = "Out Of Service", Text = "Out Of Service" },
            }, "Value", "Text");
            var liftingBridge = await _liftingBridgeServices.GetByIdAsyncServiceGeneric(id);
            if (liftingBridge == null)
            {
                return NotFound();
            }
            var liftingBridgeDto = _mapper.Map<UpdateLiftingBridgeDto>(liftingBridge);
            var liftingBridgeViewModel = _mapper.Map<UpdateLiftingBridgeViewModel>(liftingBridgeDto);

            return View(liftingBridgeViewModel);
        }

        // POST: LiftingBridgeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateLiftingBridgeViewModel updateLiftingBridgeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var bridge = await _liftingBridgeServices.GetByIdAsyncServiceGeneric(updateLiftingBridgeViewModel.Id);
                    if (bridge == null)
                    {
                        return NotFound();
                    }
                    var updateLiftingBridgeDto = _mapper.Map<UpdateLiftingBridgeDto>(updateLiftingBridgeViewModel);
                    var bridgeToUpdate = _mapper.Map(updateLiftingBridgeDto, bridge);
                    bridgeToUpdate.UpdatedAt = DateTime.Now;
                    bridgeToUpdate.UpdatedBy = User.Identity?.Name;
                    await _liftingBridgeServices.UpdateAsyncServiceGeneric(bridgeToUpdate);
                    return RedirectToAction(nameof(Index));
                }
                return View(updateLiftingBridgeViewModel);
            }
            catch
            {
                return View(updateLiftingBridgeViewModel);
            }
        }

        // GET: LiftingBridgeController/Delete/5
        [Authorize(Roles = "GeneralManager")]
        public async Task<ActionResult> Delete(int id)
        {
            var liftingBridge = await _liftingBridgeServices.GetByIdAsyncServiceGeneric(id);
            if (liftingBridge == null)
            {
                return NotFound();
            }
            var liftingBridgeDto = _mapper.Map<DeleteLiftingBridgeDto>(liftingBridge);
            var liftingBridgeViewModel = _mapper.Map<DeleteLiftingBridgeViewModel>(liftingBridgeDto);

            return View(liftingBridgeViewModel);
        }

        // POST: LiftingBridgeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GeneralManager")]
        public async Task<ActionResult> Delete(DeleteLiftingBridgeViewModel deleteLiftingBridgeViewModel)
        {
            try
            {

                var existingBridge = await _liftingBridgeServices.GetByIdAsyncServiceGeneric(deleteLiftingBridgeViewModel.Id);
                if (existingBridge == null)
                {
                    return NotFound();
                }
                existingBridge.IsDeleted = true;
                existingBridge.DeletedAt = DateTime.Now;
                existingBridge.DeletedBy = User.Identity?.Name;
                await _liftingBridgeServices.UpdateAsyncServiceGeneric(existingBridge);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
