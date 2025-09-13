using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
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
        public async Task<ActionResult> Index(string searchVin, string searchModel, string searchBrand,
           string searchFuel, string searchTransmission, int pageNumber = 1, int pageSize = 50)
        {
            var totalVahicle = await _vehicleService.CountAsyncServiceGeneric();
            var vehicles = await _vehicleService.GetAllAsyncServiceGeneric();
            var activeVehicles = vehicles.Where(v => !v.IsDeleted).ToList();

            if (!string.IsNullOrWhiteSpace(searchVin))
            {
                var vehicleDto = await _vehicleService.GetVehicleByVin(searchVin.Trim());

                if (vehicleDto == null)
                {
                    TempData["VehicleError"] = "No matching VIN found.";
                    return View(Enumerable.Empty<ReadVehicleViewModel>());
                }

                var viewModel = _mapper.Map<IEnumerable<ReadVehicleViewModel>>(vehicleDto);
                ViewBag.TotalPages = 1;
                ViewBag.CurrentPage = 1;
                ViewBag.CountVehicle = 1;

                return View(viewModel);
            }
            else
            {
                var vehicleDtoss = await _vehicleService.GetVehiclesByMultipleParam(searchBrand?.Trim(),
                    searchModel?.Trim(),
                    searchFuel?.Trim(),
                    searchTransmission?.Trim());

                var paginated = vehicleDtoss
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);

                var viewModels = _mapper.Map<IEnumerable<ReadVehicleViewModel>>(paginated);

                ViewBag.TotalPages = (int)Math.Ceiling((double)vehicleDtoss.Count() / pageSize);
                ViewBag.CurrentPage = pageNumber;
                ViewBag.CountVehicle = vehicleDtoss.Count();
                ViewBag.ActiveVehicle = vehicleDtoss.Count(); // since all are active

                return View(viewModels);
            }
            var paginatedVahicles = activeVehicles.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var vehicleDtos = _mapper.Map<IEnumerable<ReadVehicleDto>>(paginatedVahicles);
            var vehicleViewModels = _mapper.Map<IEnumerable<ReadVehicleViewModel>>(vehicleDtos);
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalVahicle / pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.CountVehicle = totalVahicle;
            ViewBag.ActiveVehicle = activeVehicles.Count;
            //////////////////
            // Preserve search terms
            ViewBag.SearchVin = searchVin;
            ViewBag.SearchModel = searchModel;
            ViewBag.SearchBrand = searchBrand;
            ViewBag.SearchFuel = searchFuel;
            ViewBag.SearchTransmission = searchTransmission;

            return View(vehicleViewModels);
        }

        // GET: VehicleController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsyncServiceGeneric(id, null, c => c.Client);
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
            var res = clients.Where(c => c.IsDeleted == false).OrderBy(c => c.FirstName); ;
            ViewBag.Clients = new SelectList(res.Select(c => new
            {
                c.Id,
                FullName = c.FirstName + " " + c.LastName + " | " + c.Email
            }), "Id", "FullName");
            ViewBag.FuelTypes = new SelectList(new[]
            {
                new { Value = "Gasoline", Text = "Gasoline" },
                new { Value = "Diesel", Text = "Diesel" },
                new { Value = "Diesel50", Text = "Diesel 50" },
            }, "Value", "Text");

            ViewBag.TransmissionTypes = new SelectList(new[]
            {
                new { Value = "Manual", Text = "Manual" },
                new { Value = "Automatic", Text = "Automatic" },
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
            vehicle.CreatedAt = DateTime.Now;
            vehicle.CreatedBy = User.Identity?.Name;
            if (ModelState.IsValid)
            {
                try
                {
                    await _vehicleService.AddAsyncServiceGeneric(vehicle);
                    await _vehicleService.SaveChangesServiceGeneric();
                    if ((createVehicleViewModel.ClientId > 0))
                    {
                        return RedirectToAction("Details", "Client", new { id = createVehicleViewModel.ClientId });
                    }
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
            var vehicle = await _vehicleService.GetByIdAsyncServiceGeneric(id, null, c => c.Client);
            if (vehicle == null)
            {
                return NotFound();
            }
            var clients = await _clientServices.GetAllAsyncServiceGeneric();
            var res = clients.Where(c => c.IsDeleted == false);
            ViewBag.Clients = new SelectList(res.Select(c => new
            {
                c.Id,
                FullName = c.FirstName + " " + c.LastName + " | " + c.Email
            }), "Id", "FullName");
            ViewBag.FuelTypes = new SelectList(new[]
             {
                new { Value = "Gasoline", Text = "Gasoline" },
                new { Value = "Diesel", Text = "Diesel" },
                new { Value = "Diesel50", Text = "Diesel 50" },
            }, "Value", "Text");

            ViewBag.TransmissionTypes = new SelectList(new[]
            {
                new { Value = "Manual", Text = "Manual" },
                new { Value = "Automatic", Text = "Automatic" },
            }, "Value", "Text");
            var vehicleDto = _mapper.Map<UpdateVehicleDto>(vehicle);
            var vehicleViewModel = _mapper.Map<UpdateVehicleViewModel>(vehicleDto);
            return View(vehicleViewModel);
        }

        // POST: VehicleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateVehicleViewModel updateVehicleViewModel)
        {
            try
            {
                var vehicle = await _vehicleService.GetByIdAsyncServiceGeneric(updateVehicleViewModel.Id);
                if (vehicle == null)
                {
                    return NotFound();
                }
                var vehicleDto = _mapper.Map<UpdateVehicleDto>(updateVehicleViewModel);

                var updatedClient = _mapper.Map(vehicleDto, vehicle);
                updatedClient.UpdatedAt = DateTime.UtcNow;
                updatedClient.UpdatedBy = User.Identity?.Name;
                await _vehicleService.UpdateAsyncServiceGeneric(updatedClient);
                //await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleController/Delete/5
        [Authorize(Roles = "GeneralManager")]
        public async Task<ActionResult> Delete(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsyncServiceGeneric(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            var vehicleDto = _mapper.Map<DeleteVehicleDto>(vehicle);
            var vehicleViewModel = _mapper.Map<DeleteVehicleViewModel>(vehicleDto);

            return View(vehicleViewModel);
        }

        // POST: VehicleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GeneralManager")]
        public async Task<ActionResult> Delete(DeleteVehicleViewModel deleteVehicleViewModel)
        {
            try
            {
                var vehicle = await _vehicleService.GetByIdAsyncServiceGeneric(deleteVehicleViewModel.Id);
                if (vehicle == null)
                {
                    return NotFound();
                }
                vehicle.IsDeleted = true;
                vehicle.DeletedAt = DateTime.Now;
                vehicle.DeletedBy = User.Identity?.Name;
                await _vehicleService.UpdateAsyncServiceGeneric(vehicle);
                //_vehicleService.SaveChangesServiceGeneric();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult CreateByClientId(int clientId)
        {
            var model = new CreateVehicleViewModel { ClientId = clientId };

            ViewBag.FuelTypes = new SelectList(new[]
            {
                new { Value = "Gasoline", Text = "Gasoline" },
                new { Value = "Diesel", Text = "Diesel" },
                new { Value = "Diesel50", Text = "Diesel 50" },
            }, "Value", "Text");

            ViewBag.TransmissionTypes = new SelectList(new[]
            {
                new { Value = "Manual", Text = "Manual" },
                new { Value = "Automatic", Text = "Automatic" },
            }, "Value", "Text");
            return View("CreateByClientId", model);
        }

    }
}