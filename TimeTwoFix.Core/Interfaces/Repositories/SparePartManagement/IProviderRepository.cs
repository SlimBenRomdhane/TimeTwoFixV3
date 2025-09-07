using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Core.Interfaces.Repositories.SparePartManagement
{
    public interface IProviderRepository : IBaseRepository<Provider>
    {
        Task<Provider> GetProviderByFiscalIdAsync(string fiscalId);
    }
}
