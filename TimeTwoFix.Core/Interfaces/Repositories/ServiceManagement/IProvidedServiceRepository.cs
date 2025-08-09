using TimeTwoFix.Core.Entities.ServiceManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Core.Interfaces.Repositories.ServiceManagement
{
    public interface IProvidedServiceRepository : IBaseRepository<ProvidedService>
    {
        Task<IEnumerable<ProvidedService>> GetServicesByNameAsync(string name);
        Task<IEnumerable<ProvidedService>> GetServicesByCategoryIdAsync(int categoryId);
    }
}