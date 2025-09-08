using AutoMapper;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.SparePartServices.Dtos;
using TimeTwoFix.Application.SparePartServices.Interfaces;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.SparePartServices.Services
{
    public class SparePartService : BaseService<SparePart>, ISparePartService
    {
        public SparePartService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<ReadSparePartDto?> GetSparePartByPartCodeAsync(string partCode)
        {
            var sparePart = await _unitOfWork.SpareParts.GetSparePartByPartCode(partCode);
            if (sparePart == null)
            {
                return null;
            }
            var readSparePartDto = _mapper.Map<ReadSparePartDto>(sparePart);
            return readSparePartDto;
        }

        public async Task<IEnumerable<ReadSparePartDto>> GetSparePartsByNameAsync(string searchTerm)
        {
            var spareParts = await _unitOfWork.SpareParts.GetSparePartsByNameAsync(searchTerm);
            var readSparePartDtos = _mapper.Map<IEnumerable<ReadSparePartDto>>(spareParts);
            return readSparePartDtos;
        }
    }
}