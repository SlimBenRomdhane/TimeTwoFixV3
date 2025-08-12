using AutoMapper;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.WorkOrderService.Dtos;
using TimeTwoFix.Application.WorkOrderService.Interfaces;
using TimeTwoFix.Core.Entities.WorkOrderManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.WorkOrderService.Services
{
    public class WorkOrderService : BaseService<WorkOrder>, IWorkOrderService
    {
        public WorkOrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<ReadWorkOrderDto>> GetWorkOrdersByDateRange(DateOnly startDate, DateOnly endDate)
        {
            var workOrders = await _unitOfWork.WorkOrders.GetWorkOrdersByDateRangeAsync(startDate, endDate);
            if (workOrders == null || !workOrders.Any())
            {
                return Enumerable.Empty<ReadWorkOrderDto>();
            }
            var workOrderDtos = _mapper.Map<IEnumerable<ReadWorkOrderDto>>(workOrders);
            return workOrderDtos;



        }

        public async Task<IEnumerable<ReadWorkOrderDto>> GetWorkOrdersByStatus(string status)
        {
            var workOrders = await _unitOfWork.WorkOrders.GetWorkOrdersByStatusAsync(status);
            if (workOrders == null || !workOrders.Any())
            { return Enumerable.Empty<ReadWorkOrderDto>(); }
            var workOrderDto = _mapper.Map<IEnumerable<ReadWorkOrderDto>>(workOrders);
            return workOrderDto;
        }

        public async Task<IEnumerable<ReadWorkOrderDto>> GetWorkOrdersByVehicleId(int vehicleId)
        {
            var workOrders = await _unitOfWork.WorkOrders.GetWorkOrdersByVehicleIdAsync(vehicleId);
            if (workOrders == null || !workOrders.Any())
            {
                return (Enumerable.Empty<ReadWorkOrderDto>());
            }
            var workOrderDtos = _mapper.Map<IEnumerable<ReadWorkOrderDto>>(workOrders);
            return (workOrderDtos);
        }
    }
}
