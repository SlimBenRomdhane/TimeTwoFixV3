using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTwoFix.Application.AppointmentServices.Dtos;
using TimeTwoFix.Application.AppointmentServices.Interfaces;
using TimeTwoFix.Application.VehicleServices.Interfaces;
using TimeTwoFix.Core.Entities.AppointmentManagement;
using TimeTwoFix.Core.Interfaces;
using TimeTwoFix.Web.Models.AppointmentModels;

namespace TimeTwoFix.Web.Controllers
{
    [Authorize(Roles = "FrontDeskAssistant,GeneralManager")]
    public class AppointmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentService _appointmentService;
        private readonly IVehicleService _vehicleService;

        public AppointmentController(IUnitOfWork unitOfWork, IMapper mapper, IAppointmentService appointmentServices, IVehicleService vehicleService)
        {
            //_unitOfWork = unitOfWork;
            _mapper = mapper;
            _appointmentService = appointmentServices;
            _vehicleService = vehicleService;
        }

        // GET: AppointmentController
        public async Task<ActionResult> Index()
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsByDateAsync(DateOnly.FromDateTime(DateTime.Today));
                if (appointments == null || !appointments.Any())
                {
                    // Handle the case where no appointments are found
                    return View(Enumerable.Empty<ReadAppointmentViewModel>());
                }

                var appointmentsDto = _mapper.Map<IEnumerable<ReadAppointmentDto>>(appointments);
                var appointmentsViewModel = _mapper.Map<IEnumerable<ReadAppointmentViewModel>>(appointmentsDto);

                return View(appointmentsViewModel);
            }
            catch (Exception)
            {
                TempData["AppointmentError"] = "No appointments found for today";
                return View(Enumerable.Empty<ReadAppointmentViewModel>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(DateOnly startDate, DateOnly endDate)
        {
            if (endDate < startDate)
            {
                TempData["AppointmentError"] = "End Date must be equal to or later than Start Date.";
                return View(Enumerable.Empty<ReadAppointmentViewModel>());
            }

            try
            {
                var appointments = await _appointmentService.GetAppointmentsByDateRangeAsync(startDate, endDate);
                if (appointments == null || !appointments.Any())
                {
                    // Handle the case where no appointments are found
                    TempData["AppointmentError"] = "No appointments found for this date range";
                    return View(Enumerable.Empty<ReadAppointmentViewModel>());
                }
                var appointmentsDto = _mapper.Map<IEnumerable<ReadAppointmentDto>>(appointments);
                var appointmentsViewModel = _mapper.Map<IEnumerable<ReadAppointmentViewModel>>(appointmentsDto);
                return View(appointmentsViewModel);
            }
            catch (Exception ex)
            {
                TempData["AppointmentError"] = ex.Message;
                return View(Enumerable.Empty<ReadAppointmentViewModel>());
            }
        }

        // GET: AppointmentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var appointment = await _appointmentService.GetByIdAsyncServiceGeneric(id, null, a => a.Vehicle);
            if (appointment == null)
            {
                return NotFound();
            }
            var appointmentDto = _mapper.Map<ReadAppointmentDto>(appointment);
            var appointmentViewModel = _mapper.Map<ReadAppointmentViewModel>(appointmentDto);
            return View(appointmentViewModel);
        }

        // GET: AppointmentController/Create
        public async Task<ActionResult> Create()
        {
            var vehicles = await _vehicleService.GetAllAsyncServiceGeneric();
            ViewBag.Vehicles = new SelectList(vehicles.OrderBy(o => o.Vin), "Id", "Vin");
            ViewBag.StatusList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Scheduled", Value = "Scheduled" },
                new SelectListItem { Text = "Confirmed", Value = "Confirmed" },
                new SelectListItem { Text = "Checked in", Value = "Checked in" },
                new SelectListItem { Text = "No-Show", Value = "No-Show" },
                new SelectListItem { Text = "Canceled", Value = "Canceled" },
                new SelectListItem { Text = "Rescheduled", Value = "Rescheduled" }
            };
            ViewBag.UrgencyLevels = new List<SelectListItem>
            {
                new SelectListItem { Text = "Low", Value = "Low" },
                new SelectListItem { Text = "Medium", Value = "Medium" },
                new SelectListItem { Text = "High", Value = "High" },
                new SelectListItem { Text = "Critical", Value = "Critical" }
            };

            return View();
        }

        // POST: AppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAppointmentViewModel createAppointmentViewModel)
        {
            var vehicles = await _vehicleService.GetAllAsyncServiceGeneric();
            ViewBag.Vehicles = new SelectList(vehicles.OrderBy(o => o.Vin), "Id", "Vin");
            ViewBag.StatusList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Scheduled", Value = "Scheduled" },
                new SelectListItem { Text = "Confirmed", Value = "Confirmed" },
                new SelectListItem { Text = "Checked in", Value = "Checked in" },
                new SelectListItem { Text = "No-Show", Value = "No-Show" },
                new SelectListItem { Text = "Canceled", Value = "Canceled" },
                new SelectListItem { Text = "Rescheduled", Value = "Rescheduled" }
            };
            ViewBag.UrgencyLevels = new List<SelectListItem>
            {
                new SelectListItem { Text = "Low", Value = "Low" },
                new SelectListItem { Text = "Medium", Value = "Medium" },
                new SelectListItem { Text = "High", Value = "High" },
                new SelectListItem { Text = "Critical", Value = "Critical" }
            };
            if (await _appointmentService.IsAppointmentAvailableAsync(createAppointmentViewModel.AppointmentDate,
                createAppointmentViewModel.AppointmentTime, 30))
            {
                var appointmentDto = _mapper.Map<CreateAppointmentDto>(createAppointmentViewModel);
                var appointment = _mapper.Map<Appointment>(appointmentDto);
                appointment.CreatedBy = User.Identity?.Name;
                appointment.CreatedAt = DateTime.Now;
                try
                {
                    if (ModelState.IsValid)
                    {
                        await _appointmentService.AddAsyncServiceGeneric(appointment);
                        return RedirectToAction(nameof(Index));
                    }
                    // If model state is invalid, return the view with the current model to show validation errors
                    return View(createAppointmentViewModel);
                }
                catch (Exception ex)
                {
                    // Log the exception (not shown here) and return the view with an error message
                    ModelState.AddModelError("", "An error occurred while creating the appointment: " + ex.Message);
                    return View(createAppointmentViewModel);
                }
            }
            else
            {
                ModelState.AddModelError("", "The selected appointment time is not available. Please choose a different time.");
                // If the appointment is not available, return the view with the current model to show the error
                return View(createAppointmentViewModel);
            }
        }

        // GET: AppointmentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var appointment = await _appointmentService.GetByIdAsyncServiceGeneric(id, null, v => v.Vehicle);
            if (appointment == null)
            {
                return NotFound();
            }
            var vehicles = await _vehicleService.GetAllAsyncServiceGeneric();
            ViewBag.Vehicles = new SelectList(vehicles.OrderBy(o => o.Vin), "Id", "Vin");
            ViewBag.StatusList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Scheduled", Value = "Scheduled" },
                new SelectListItem { Text = "Confirmed", Value = "Confirmed" },
                new SelectListItem { Text = "Checked in", Value = "Checked in" },
                new SelectListItem { Text = "No-Show", Value = "No-Show" },
                new SelectListItem { Text = "Canceled", Value = "Canceled" },
                new SelectListItem { Text = "Rescheduled", Value = "Rescheduled" }
            };
            ViewBag.UrgencyLevels = new List<SelectListItem>
            {
                new SelectListItem { Text = "Low", Value = "Low" },
                new SelectListItem { Text = "Medium", Value = "Medium" },
                new SelectListItem { Text = "High", Value = "High" },
                new SelectListItem { Text = "Critical", Value = "Critical" }
            };
            var appointmentDto = _mapper.Map<UpdateAppointmentDto>(appointment);
            var appointmentViewModel = _mapper.Map<UpdateAppointmentViewModel>(appointmentDto);
            return View(appointmentViewModel);
        }

        // POST: AppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateAppointmentViewModel updateAppointmentViewModel)
        {
            try
            {
                var appointment = await _appointmentService.GetByIdAsyncServiceGeneric(updateAppointmentViewModel.Id);
                if (appointment == null)
                {
                    return NotFound();
                }

                var appointmentDto = _mapper.Map<UpdateAppointmentDto>(updateAppointmentViewModel);
                var updatedAppointment = _mapper.Map(appointmentDto, appointment);
                updatedAppointment.UpdatedBy = User.Identity?.Name;
                updatedAppointment.UpdatedAt = DateTime.Now;
                await _appointmentService.UpdateAsyncServiceGeneric(updatedAppointment);
                //await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(updateAppointmentViewModel);
            }
        }

        // GET: AppointmentController/Delete/5
        [Authorize(Roles = "GeneralManager")]
        public async Task<ActionResult> Delete(int id)
        {
            var appointment = await _appointmentService.GetByIdAsyncServiceGeneric(id);
            if (appointment == null)
            {
                return NotFound();
            }
            var appointmentDto = _mapper.Map<DeleteAppointmentDto>(appointment);
            var appointmentViewModel = _mapper.Map<DeleteAppointmentViewModel>(appointmentDto);
            return View(appointmentViewModel);
        }

        // POST: AppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GeneralManager")]
        public async Task<ActionResult> Delete(DeleteAppointmentViewModel deleteAppointmentViewModel)
        {
            try
            {
                var appointment = await _appointmentService.GetByIdAsyncServiceGeneric(deleteAppointmentViewModel.Id);
                if (appointment == null)
                {
                    return NotFound();
                }
                appointment.IsDeleted = true;
                appointment.DeletedBy = User.Identity?.Name;
                appointment.DeletedAt = DateTime.Now;
                await _appointmentService.UpdateAsyncServiceGeneric(appointment);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}