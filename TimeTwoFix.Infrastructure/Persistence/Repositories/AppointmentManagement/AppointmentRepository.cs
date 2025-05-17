using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.AppointmentManagement;
using TimeTwoFix.Core.Interfaces.Repositories.AppointmentManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.AppointmentManagement
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateOnly date)
        {
            var appointments = _context.Appointments
                .Where(a => a.AppointmentDate == date)
                .ToListAsync();
            return await appointments;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateRangeAsync(DateOnly startDate, DateOnly endDate)
        {
            var appointments = _context.Appointments
                .Where(a => a.AppointmentDate >= startDate && a.AppointmentDate <= endDate)
                .ToListAsync();
            return await appointments;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByStatusAsync(string status)
        {
            var appointments = _context.Appointments
                .Where(a => a.Status == status)
                .ToListAsync();
            return await appointments;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByVehicleIdAsync(int vehicleId)
        {
            var appointments = _context.Appointments
                .Where(a => a.VehicleId == vehicleId)
                .ToListAsync();
            return await appointments;
        }
    }
}