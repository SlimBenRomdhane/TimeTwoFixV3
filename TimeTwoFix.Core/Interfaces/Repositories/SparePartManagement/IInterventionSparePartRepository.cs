using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Core.Interfaces.Repositories.SparePartManagement
{
    public interface IInterventionSparePartRepository : IBaseRepository<InterventionSparePart>
    {
        Task<IEnumerable<InterventionSparePart>> GetInterventionSparePartsByInterventionIdAsync(int interventionId);

        Task<IEnumerable<InterventionSparePart>> GetInterventionSparePartsBySparePartIdAsync(int sparePartId);
    }
}