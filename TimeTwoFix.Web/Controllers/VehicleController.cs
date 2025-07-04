using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using TimeTwoFix.Application.ClientServices.Interfaces;
using TimeTwoFix.Application.VehicleServices.Dtos;
using TimeTwoFix.Application.VehicleServices.Interfaces;
using TimeTwoFix.Core.Entities.VehicleManagement;
using TimeTwoFix.Web.Models.VehicleModels;

namespace TimeTwoFix.Web.Controllers
{
    [Authorize(Roles = "FrontDeskAssistant,GeneralManager")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IClientServices _clientServices;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleService vehicleService, IClientServices clientServices, IMapper mapper)
        {
            _clientServices = clientServices;
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        // GET: VehicleController
        public async Task<ActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var totalVahicle = _vehicleService.CountAsyncServiceGeneric();
            var vehicles = await _vehicleService.GetAllAsyncServiceGeneric();
            var paginatedVahicles = vehicles.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var vehicleDtos = _mapper.Map<IEnumerable<ReadVehicleDto>>(paginatedVahicles);
            var vehicleViewModels = _mapper.Map<IEnumerable<ReadVehicleViewModel>>(vehicleDtos);
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalVahicle / pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.CountVehicle = totalVahicle;

            return View(vehicleViewModels);
        }

        // GET: VehicleController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsyncServiceGeneric(id, c => c.Client);
            if (vehicle == null)
            {
                return NotFound();
            }
            var vehicleDto = _mapper.Map<ReadVehicleDto>(vehicle);
            var vehicleViewModel = _mapper.Map<ReadVehicleViewModel>(vehicleDto);
            return View(vehicleViewModel);
        }

        // GET: VehicleController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var clients = await _clientServices.GetAllAsyncServiceGeneric();
            var res = clients.Where(c => c.IsDeleted == false);
            ViewBag.Clients = new SelectList(res.Select(c => new
            {
                c.Id,
                FullName = c.FirstName + " " + c.LastName + " | " + c.Email
            }), "Id", "FullName");
            ViewBag.FuelTypes = new SelectList(new[]
            {
                new { Value = "Gasoline", Text = "Essence Sans Plomb" },
                new { Value = "Diesel", Text = "Gasoil" },
                new { Value = "Diesel50", Text = "Gasoil 50" },
                new { Value = "GPL", Text = "Gaz de Pétrole Liquéfié (GPL)" }
            }, "Value", "Text");

            ViewBag.TransmissionTypes = new SelectList(new[]
            {
                new { Value = "Manual", Text = "Manuelle" },
                new { Value = "Automatic", Text = "Automatique" },
                new { Value = "CVT", Text = "Transmission à variation continue (CVT)" },
                new { Value = "AMT", Text = "Transmission manuelle automatisée (AMT)" }
            }, "Value", "Text");
            return View();
        }

        // POST: VehicleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVehicleViewModel createVehicleViewModel)
        {
            var vehicleDto = _mapper.Map<CreateVehicleDto>(createVehicleViewModel);
            var vehicle = _mapper.Map<Vehicle>(vehicleDto);
            vehicle.CreatedAt = DateTime.UtcNow;
            vehicle.CreatedBy = User.Identity?.Name;
            if (ModelState.IsValid)
            {
                try
                {
                    await _vehicleService.AddAsyncServiceGeneric(vehicle);
                    await _vehicleService.SaveChangesServiceGeneric();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "An error occurred while creating the vehicle.");
                    // If an error occurs, re-populate the dropdown lists

                }

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: VehicleController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsyncServiceGeneric(id, c => c.Client);
            if (vehicle == null)
            {
                return NotFound();
            }
            var vehicleDto = _mapper.Map<UpdateVehicleDto>(vehicle);
            var vehicleViewModel = _mapper.Map<UpdateVehicleViewModel>(vehicleDto);


            return View(vehicleViewModel);
        }

        // POST: VehicleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehicleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}