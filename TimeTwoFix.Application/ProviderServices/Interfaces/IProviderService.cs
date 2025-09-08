using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.ProviderServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;

namespace TimeTwoFix.Application.ProviderServices.Interfaces
{
    public interface IProviderService : IBaseService<Provider>
    {
        Task<ReadProviderDto> GetProviderByFiscalIdAsync(string fiscalId);
    }
}