using AutoMapper;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.ProvidedServicesService.Dtos;
using TimeTwoFix.Application.ProvidedServicesService.Interfaces;
using TimeTwoFix.Core.Entities.ServiceManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.ProvidedServicesService.Services
{
    public class ProvidedServiceService : BaseService<ProvidedService>, IProvidedServiceService
    {
        public ProvidedServiceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<ReadProvidedServiceDto>> GetServicesByCategoryIdAsync(int categoryId)
        {
            var services = await _unitOfWork.ProvidedServices.GetByIdAsyncGeneric(categoryId);
            if (services == null)
            {
                throw new Exception("No services found for the given category ID");
            }
            var serviceDtos = _mapper.Map<IEnumerable<ReadProvidedServiceDto>>(services);
            return serviceDtos;
        }

        public async Task<IEnumerable<ReadProvidedServiceDto>> GetServicesByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Service name cannot be null or empty", nameof(name));
            }
            var services = await _unitOfWork.ProvidedServices.GetServicesByNameAsync(name);
            if (services == null || !services.Any())
            {
                throw new Exception("No services found with the given name");
            }
            var serviceDtos = _mapper.Map<IEnumerable<ReadProvidedServiceDto>>(services);
            return serviceDtos;
        }
    }
}
