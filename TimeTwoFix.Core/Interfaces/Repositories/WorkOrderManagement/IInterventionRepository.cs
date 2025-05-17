using TimeTwoFix.Core.Entities.WorkOrderManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Core.Interfaces.Repositories.WorkOrderManagement
{
    public interface IInterventionRepository : IBaseRepository<Intervention>
    {
        Task<IEnumerable<Intervention>> GetInterventionsByWorkOrderIdAsync(int workOrderId);

        Task<IEnumerable<Intervention>> GetInterventionsByMechanicIdAsync(int mechanicId);

        Task<IEnumerable<Intervention>> GetInterventionsByServiceIdAsync(int serviceId);

        Task<IEnumerable<Intervention>> GetInterventionsByLiftingBridgeIdAsync(int liftingBridgeId);

        Task<IEnumerable<Intervention>> GetInterventionsByStatusAsync(string status);

        Task<IEnumerable<Intervention>> GetInterventionsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}