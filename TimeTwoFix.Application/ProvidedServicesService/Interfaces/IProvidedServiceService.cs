using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.ProvidedServicesService.Dtos;
using TimeTwoFix.Core.Entities.ServiceManagement;

namespace TimeTwoFix.Application.ProvidedServicesService.Interfaces
{
    public interface IProvidedServiceService : IBaseService<ProvidedService>
    {
        Task<IEnumerable<ReadProvidedServiceDto>> GetServicesByNameAsync(string name);

        Task<IEnumerable<ReadProvidedServiceDto>> GetServicesByCategoryIdAsync(int categoryId);
    }
}