using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.InterventionService.Dtos;
using TimeTwoFix.Core.Entities.WorkOrderManagement;

namespace TimeTwoFix.Application.InterventionService.Interfaces
{
    public interface IInterventionService : IBaseService<Intervention>
    {
        Task<IEnumerable<ReadInterventionDto>> GetInterventionsByWorkOrderId(int workOrderId);

        Task<IEnumerable<ReadInterventionDto>> GetInterventionsByMechanicId(int mechanicId);

        Task<IEnumerable<ReadInterventionDto>> GetInterventionsByServiceId(int serviceId);

        Task<IEnumerable<ReadInterventionDto>> GetInterventionsByLiftingBridgeId(int liftingBridgeId);

        Task<IEnumerable<ReadInterventionDto>> GetInterventionsByStatus(string status);

        Task<IEnumerable<ReadInterventionDto>> GetInterventionsByDateRange(DateTime startDate, DateTime endDate);
    }
}
