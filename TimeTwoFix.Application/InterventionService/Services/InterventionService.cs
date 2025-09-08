using AutoMapper;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.InterventionService.Dtos;
using TimeTwoFix.Application.InterventionService.Interfaces;
using TimeTwoFix.Core.Entities.WorkOrderManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.InterventionService.Services
{
    public class InterventionService : BaseService<Intervention>, IInterventionService
    {
        public InterventionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<ReadInterventionDto>> GetInterventionsByDateRange(DateTime startDate, DateTime endDate)
        {
            var interventions = await _unitOfWork.Interventions.GetInterventionsByDateRangeAsync(startDate, endDate);
            if (interventions == null || !interventions.Any())
            {
                return Enumerable.Empty<ReadInterventionDto>();
            }
            var interventionDtos = _mapper.Map<IEnumerable<ReadInterventionDto>>(interventions);
            return interventionDtos;
        }

        public async Task<IEnumerable<ReadInterventionDto>> GetInterventionsByLiftingBridgeId(int liftingBridgeId)
        {
            var interventions = await _unitOfWork.Interventions.GetInterventionsByLiftingBridgeIdAsync(liftingBridgeId);
            if (interventions == null || !interventions.Any())
            {
                return Enumerable.Empty<ReadInterventionDto>();
            }
            var interventionDtos = _mapper.Map<IEnumerable<ReadInterventionDto>>(interventions);
            return interventionDtos;
        }

        public async Task<IEnumerable<ReadInterventionDto>> GetInterventionsByMechanicId(int mechanicId)
        {
            var interventions = await _unitOfWork.Interventions.GetInterventionsByMechanicIdAsync(mechanicId);
            if (interventions == null || !interventions.Any())
            {
                return Enumerable.Empty<ReadInterventionDto>();
            }
            var interventionDtos = _mapper.Map<IEnumerable<ReadInterventionDto>>(interventions);
            return interventionDtos;
        }

        public async Task<IEnumerable<ReadInterventionDto>> GetInterventionsByServiceId(int serviceId)
        {
            var interventions = await _unitOfWork.Interventions.GetInterventionsByServiceIdAsync(serviceId);
            if (interventions == null || !interventions.Any())
            {
                return Enumerable.Empty<ReadInterventionDto>();
            }
            var interventionDtos = _mapper.Map<IEnumerable<ReadInterventionDto>>(interventions);
            return interventionDtos;
        }

        public async Task<IEnumerable<ReadInterventionDto>> GetInterventionsByStatus(string status)
        {
            var interventions = await _unitOfWork.Interventions.GetInterventionsByStatusAsync(status);
            if (interventions == null || !interventions.Any())
            {
                return Enumerable.Empty<ReadInterventionDto>();
            }
            var interventionDtos = _mapper.Map<IEnumerable<ReadInterventionDto>>(interventions);
            return interventionDtos;
        }

        public async Task<IEnumerable<ReadInterventionDto>> GetInterventionsByWorkOrderId(int workOrderId)
        {
            var interventions = await _unitOfWork.Interventions.GetInterventionsByWorkOrderIdAsync(workOrderId);
            if (interventions == null || !interventions.Any())
            {
                return Enumerable.Empty<ReadInterventionDto>();
            }
            var interventionDtos = _mapper.Map<IEnumerable<ReadInterventionDto>>(interventions);
            return interventionDtos;
        }
    }
}