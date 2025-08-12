using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTwoFix.Application.WorkOrderService.Dtos;
using TimeTwoFix.Core.Entities.WorkOrderManagement;

namespace TimeTwoFix.Application.WorkOrderService.Interfaces
{
    public interface IWorkOrderService
    {
        Task<IEnumerable<ReadWorkOrderDto>> GetWorkOrdersByStatus(string status);

        Task<IEnumerable<ReadWorkOrderDto>> GetWorkOrdersByVehicleId(int vehicleId);

        Task<IEnumerable<ReadWorkOrderDto>> GetWorkOrdersByDateRange(DateOnly startDate, DateOnly endDate);
    }
}
