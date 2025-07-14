using AutoMapper;
using TimeTwoFix.Application.AppointmentServices.Dtos;
using TimeTwoFix.Application.AppointmentServices.Interfaces;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Core.Entities.AppointmentManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.AppointmentServices.Services
{
    public class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<ReadAppointmentDto>> GetAppointmentsByDateAsync(DateOnly date)
        {

            var appointments = await _unitOfWork.Appointments.GetAppointmentsByDateAsync(date);
            if (appointments == null || !appointments.Any())
            {
                throw new Exception("No appointments found for the specified date.");
            }
            var readAppointmentDtos = _mapper.Map<IEnumerable<ReadAppointmentDto>>(appointments);
            return readAppointmentDtos;
        }

        public async Task<IEnumerable<ReadAppointmentDto>> GetAppointmentsByDateRangeAsync(DateOnly startDate, DateOnly endDate)
        {
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByDateRangeAsync(startDate, endDate);
            if (appointments == null || !appointments.Any())
            {
                throw new Exception("No appointments found for the specified date range.");
            }
            var readAppointmentDtos = _mapper.Map<IEnumerable<ReadAppointmentDto>>(appointments);
            return readAppointmentDtos;

        }

        public async Task<IEnumerable<ReadAppointmentDto>> GetAppointmentsByStatusAsync(string status)
        {
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByStatusAsync(status);
            if (appointments == null || !appointments.Any())
            {
                throw new Exception("No appointments found for the specified status.");
            }
            var readAppointmentDtos = _mapper.Map<IEnumerable<ReadAppointmentDto>>(appointments);
            return readAppointmentDtos;

        }

        public async Task<IEnumerable<ReadAppointmentDto>> GetAppointmentsByVehicleIdAsync(int vehicleId)
        {
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByVehicleIdAsync(vehicleId);
            if (appointments == null || !appointments.Any())
            {
                throw new Exception("No appointments found for the specified vehicle ID.");
            }
            var readAppointmentDtos = _mapper.Map<IEnumerable<ReadAppointmentDto>>(appointments);
            return readAppointmentDtos;
        }

        public async Task<bool> IsAppointmentAvailableAsync(DateOnly dateOnly, TimeOnly newDate, int intervall = 30)
        {
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByDateAsync(dateOnly);
            if (appointments == null || !appointments.Any())
            {
                return true; // No appointments, so it's available
            }
            foreach (var appointment in appointments)
            {
                var appointmentTime = appointment.AppointmentTime;
                var startTime = appointmentTime.AddMinutes(-intervall);
                var endTime = appointmentTime.AddMinutes(intervall);
                if (newDate >= startTime && newDate <= endTime)
                {
                    return false; // Appointment time overlaps
                }
            }
            return true; // No overlaps found, it's available
        }
    }
}
