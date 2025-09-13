using AutoMapper;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.ProviderServices.Dtos;
using TimeTwoFix.Application.ProviderServices.Interfaces;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.ProviderServices.Services
{
    public class ProviderService : BaseService<Provider>, IProviderService
    {
        public ProviderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<ReadProviderDto> GetProviderByFiscalIdAsync(string fiscalId)
        {
            var provider = await _unitOfWork.Providers.GetProviderByFiscalIdAsync(fiscalId);
            if (provider == null)
            {
                return null;
            }
            var readProviderDto = _mapper.Map<ReadProviderDto>(provider);
            return readProviderDto;
        }

        public async Task<IEnumerable<ReadProviderDto>> GetProviderByNameAsync(string name)
        {
            var providers = await _unitOfWork.Providers.GetProviderByNameAsync(name);
            if (providers == null)
            {
                return Enumerable.Empty<ReadProviderDto>();
            }
            return _mapper.Map<IEnumerable<ReadProviderDto>>(providers);
        }
    }
}