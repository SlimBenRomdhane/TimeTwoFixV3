using TimeTwoFix.Core.Entities.AppointmentManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Core.Interfaces.Repositories.AppointmentManagement
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateOnly date);

        Task<IEnumerable<Appointment>> GetAppointmentsByVehicleIdAsync(int vehicleId);

        Task<IEnumerable<Appointment>> GetAppointmentsByStatusAsync(string status);

        Task<IEnumerable<Appointment>> GetAppointmentsByDateRangeAsync(DateOnly startDate, DateOnly endDate);
    }
}