using TimeTwoFix.Core.Entities.WorkOrderManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Core.Interfaces.Repositories.WorkOrderManagement
{
    public interface IWorkOrderRepository : IBaseRepository<WorkOrder>
    {
        Task<IEnumerable<WorkOrder>> GetWorkOrdersByStatusAsync(string status);

        Task<IEnumerable<WorkOrder>> GetWorkOrdersByVehicleIdAsync(int vehicleId);

        Task<IEnumerable<WorkOrder>> GetWorkOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}