using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Core.Interfaces.Repositories.SparePartManagement
{
    public interface IProviderSparePartRepository : IBaseRepository<ProviderSparePart>
    {
        Task<IEnumerable<ProviderSparePart>> GetProviderSparePartsByProviderIdAsync(int providerId);
        Task<IEnumerable<ProviderSparePart>> GetProviderSparePartsBySparePartIdAsync(int sparePartId);
    }
}
