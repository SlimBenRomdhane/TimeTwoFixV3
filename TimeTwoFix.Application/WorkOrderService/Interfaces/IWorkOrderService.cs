using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.WorkOrderService.Dtos;
using TimeTwoFix.Core.Entities.WorkOrderManagement;

namespace TimeTwoFix.Application.WorkOrderService.Interfaces
{
    public interface IWorkOrderService : IBaseService<WorkOrder>
    {
        Task<IEnumerable<ReadWorkOrderDto>> GetWorkOrdersByStatus(string status);

        Task<IEnumerable<ReadWorkOrderDto>> GetWorkOrdersByVehicleId(int vehicleId);

        Task<IEnumerable<ReadWorkOrderDto>> GetWorkOrdersByDateRange(DateTime startDate, DateTime endDate);
    }
}