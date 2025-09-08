using AutoMapper;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.InterventionSparePartServices.Dtos;
using TimeTwoFix.Application.InterventionSparePartServices.Interfaces;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.InterventionSparePartServices.Services
{
    public class InterventionSparePartService : BaseService<InterventionSparePart>, IInterventionSparePartService
    {
        public InterventionSparePartService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<ReadInterventionSparePartDto>> GetByInterventionSparePartByInterventionIdAsync(int interventionId)
        {
            var interventionSparePart = await _unitOfWork.InterventionSpareParts.GetInterventionSparePartsByInterventionIdAsync(interventionId);
            if (interventionSparePart == null || !interventionSparePart.Any())
            {
                return Enumerable.Empty<ReadInterventionSparePartDto>();
            }
            var dto = _mapper.Map<IEnumerable<ReadInterventionSparePartDto>>(interventionSparePart);
            return dto;
        }

        public async Task<IEnumerable<ReadInterventionSparePartDto>> GetByInterventionSparePartBySparePartIdAsync(int sparePartId)
        {
            var interventionSparePart = await _unitOfWork.InterventionSpareParts.GetInterventionSparePartsBySparePartIdAsync(sparePartId);
            if (interventionSparePart == null || !interventionSparePart.Any())
            {
                return Enumerable.Empty<ReadInterventionSparePartDto>();
            }
            var dto = _mapper.Map<IEnumerable<ReadInterventionSparePartDto>>(interventionSparePart);
            return dto;
        }
    }
}