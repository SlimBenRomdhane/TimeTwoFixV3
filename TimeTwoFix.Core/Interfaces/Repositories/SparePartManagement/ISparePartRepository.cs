using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Core.Interfaces.Repositories.SparePartManagement
{
    public interface ISparePartRepository : IBaseRepository<SparePart>
    {
        Task<IEnumerable<SparePart>> GetSparePartsByNameAsync(string name);

        Task<SparePart?> GetSparePartByPartCode(string partCode);

        Task<IEnumerable<SparePart>> GetSparePartsByQuantityAsync(int quantity);
    }
}