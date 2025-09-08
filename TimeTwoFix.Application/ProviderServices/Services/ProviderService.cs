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
    }
}