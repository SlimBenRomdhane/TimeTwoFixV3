using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.WorkOrderManagement;
using TimeTwoFix.Core.Interfaces.Repositories.WorkOrderManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.WorkOrderManagement
{
    public class WorkOrderRepository : BaseRepository<WorkOrder>, IWorkOrderRepository
    {
        public WorkOrderRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WorkOrder>> GetWorkOrdersByStatusAsync(string status)
        {
            var workOrders = await _context.WorkOrders
                .Where(w => w.Status.Contains(status))
                .ToListAsync();
            return workOrders;
        }

        public async Task<IEnumerable<WorkOrder>> GetWorkOrdersByVehicleIdAsync(int vehicleId)
        {
            var workOrders = await _context.WorkOrders
                .Where(w => w.VehicleId == vehicleId)
                .ToListAsync();
            return workOrders;
        }

        public async Task<IEnumerable<WorkOrder>> GetWorkOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var workOrders = await _context.WorkOrders
                .Where(w => w.StartDate >= startDate && w.EndDate <= endDate)
                .ToListAsync();
            return workOrders;
        }
    }
}