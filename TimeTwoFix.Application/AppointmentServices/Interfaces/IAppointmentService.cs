using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeTwoFix.Application.AppointmentServices.Dtos;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Core.Entities.AppointmentManagement;

namespace TimeTwoFix.Application.AppointmentServices.Interfaces
{
    public interface IAppointmentService : IBaseService<Appointment>
    {
        public Task<IEnumerable<ReadAppointmentDto>> GetAppointmentsByDateAsync(DateOnly date);
        public Task<IEnumerable<ReadAppointmentDto>> GetAppointmentsByDateRangeAsync(DateOnly startDate, DateOnly endDate);
        public Task<IEnumerable<ReadAppointmentDto>> GetAppointmentsByStatusAsync(string status);
        public Task<IEnumerable<ReadAppointmentDto>> GetAppointmentsByVehicleIdAsync(int vehicleId);
        public Task<bool> IsAppointmentAvailableAsync(DateOnly dateOnly, TimeOnly newDate, int intervall = 30);
    }
}
