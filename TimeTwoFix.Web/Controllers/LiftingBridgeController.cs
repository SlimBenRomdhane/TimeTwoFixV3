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
    [Authorize(Roles = "GeneralManager")]
    public class LiftingBridgeController : BaseController<LiftingBridge, CreateLiftingBridgeDto, ReadLiftingBridgeDto, UpdateLiftingBridgeDto, DeleteLiftingBridgeDto,
        CreateLiftingBridgeViewModel, ReadLiftingBridgeViewModel, UpdateLiftingBridgeViewModel, DeleteLiftingBridgeViewModel>
    {
        private readonly ILiftingBridgeServices _liftingBridgeServices;

        public LiftingBridgeController(IMapper mapper, ILiftingBridgeServices liftingBridgeServices) : base(liftingBridgeServices, mapper)
        {
            _liftingBridgeServices = liftingBridgeServices;
        }

        // GET: LiftingBridgeController/Create
        // This method overrides an async base method and must retain the async signature.
        // Although no await is used here, async is required for compatibility with the base class.
        // Suppressing CS1998 to avoid misleading compiler warnings — async behavior may be added later.
#pragma warning disable CS1998

        [HttpGet]
        public override async Task<ActionResult> Create()
        {
            ViewBag.BridgeStatus = new SelectList(new[]
            {
                new { Value = "Idle", Text = "Idle" },
                new { Value = "Occupied", Text = "Occupied" },
                new { Value = "Out Of Service", Text = "Out Of Service" },
            }, "Value", "Text");
            return View();
        }

#pragma warning restore CS1998

        // GET: LiftingBridgeController/Edit/5
        [HttpGet]
        public override async Task<IActionResult> Edit(int id)
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

        // Additional controller actions go here
    }
}