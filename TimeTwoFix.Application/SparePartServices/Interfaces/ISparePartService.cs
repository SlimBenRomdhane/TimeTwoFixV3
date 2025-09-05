using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.SparePartServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;

namespace TimeTwoFix.Application.SparePartServices.Interfaces
{
    public interface ISparePartService : IBaseService<SparePart>
    {
        Task<IEnumerable<ReadSparePartDto>> GetSparePartsByNameAsync(string searchTerm);
        Task<ReadSparePartDto?> GetSparePartByPartCodeAsync(string partCode);
    }
}
