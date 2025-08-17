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
    [Authorize(Roles = "GeneralManager,WorkshopManager")]
    public class LiftingBridgeController : BaseController<LiftingBridge, CreateLiftingBridgeDto, ReadLiftingBridgeDto, UpdateLiftingBridgeDto, DeleteLiftingBridgeDto,
        CreateLiftingBridgeViewModel, ReadLiftingBridgeViewModel, UpdateLiftingBridgeViewModel, DeleteLiftingBridgeViewModel>
    {

        private readonly ILiftingBridgeServices _liftingBridgeServices;

        public LiftingBridgeController(IMapper mapper, ILiftingBridgeServices liftingBridgeServices) : base(liftingBridgeServices, mapper)
        {

            _liftingBridgeServices = liftingBridgeServices;
        }

        // GET: LiftingBridgeController/Create
        [HttpGet]
        override public ActionResult Create()
        {
            ViewBag.BridgeStatus = new SelectList(new[]
            {
                new { Value = "Idle", Text = "Idle" },
                new { Value = "Occupied", Text = "Occupied" },
                new { Value = "Out Of Service", Text = "Out Of Service" },
            }, "Value", "Text");
            return View();
        }

        // GET: LiftingBridgeController/Edit/5
        [HttpGet]
        override public async Task<IActionResult> Edit(int id)
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
