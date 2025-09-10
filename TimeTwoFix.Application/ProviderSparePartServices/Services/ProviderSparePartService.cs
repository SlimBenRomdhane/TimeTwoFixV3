using AutoMapper;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.ProviderSparePartServices.Dtos;
using TimeTwoFix.Application.ProviderSparePartServices.Interfaces;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.ProviderSparePartServices.Services
{
    public class ProviderSparePartService : BaseService<ProviderSparePart>, IProviderSparePartService
    {
        public ProviderSparePartService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<ReadProviderSparePartDto>> GetProviderSparePartsByProviderIdService(int providerId)
        {
            var providerSpareParts = await _unitOfWork.ProviderSpareParts.GetProviderSparePartsByProviderIdAsync(providerId);
            return _mapper.Map<IEnumerable<ReadProviderSparePartDto>>(providerSpareParts);
        }

        public async Task<IEnumerable<ReadProviderSparePartDto>> GetProviderSparePartsBySparePartIdService(int sparePartId)
        {
            var providerSpareParts = await _unitOfWork.ProviderSpareParts.GetProviderSparePartsBySparePartIdAsync(sparePartId);
            return _mapper.Map<IEnumerable<ReadProviderSparePartDto>>(providerSpareParts);
        }
    }
}
