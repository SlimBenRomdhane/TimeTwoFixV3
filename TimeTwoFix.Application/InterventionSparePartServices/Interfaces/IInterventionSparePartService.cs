using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.InterventionSparePartServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;

namespace TimeTwoFix.Application.InterventionSparePartServices.Interfaces
{
    public interface IInterventionSparePartService : IBaseService<InterventionSparePart>
    {
        Task<IEnumerable<ReadInterventionSparePartDto>> GetByInterventionSparePartByInterventionIdAsync(int interventionId);

        Task<IEnumerable<ReadInterventionSparePartDto>> GetByInterventionSparePartBySparePartIdAsync(int sparePartId);
    }
}