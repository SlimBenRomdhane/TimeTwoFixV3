using AutoMapper;
using TimeTwoFix.Application.ReportingServices.Dtos;
using TimeTwoFix.Application.ReportingServices.Interfaces;
using TimeTwoFix.Core.Interfaces.Repositories.ReportingManagement;

namespace TimeTwoFix.Application.ReportingServices.Services
{
    public class ReportingService : IReportingService
    {
        private readonly IReportingRepository _reportingRepository;
        private readonly IMapper _mapper;
        public ReportingService(IReportingRepository reportingRepository, IMapper mapper)
        {
            _reportingRepository = reportingRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MechanicPerformanceDto>> GetMechanicPerformanceAsync(DateTime from, DateTime to)
        {
            var result = await _reportingRepository.GetMechanicPerformanceAsync(from, to);
            var dto = _mapper.Map<IEnumerable<MechanicPerformanceDto>>(result);
            return dto;

        }

        public async Task<IEnumerable<MechanicPerformanceTrendDto>> GetMechanicPerformanceTrendAsync(DateTime from, DateTime to)
        {
            var result = await _reportingRepository.GetMechanicPerformanceTrendAsync(from, to);
            var dto = _mapper.Map<IEnumerable<MechanicPerformanceTrendDto>>(result);
            return dto;
        }

        public async Task<IEnumerable<PauseAnalysisDto>> GetPauseAnalysisAsync(DateTime from, DateTime to)
        {
            var result = await _reportingRepository.GetPauseAnalysisAsync(from, to);
            var dto = _mapper.Map<IEnumerable<PauseAnalysisDto>>(result);
            return dto;
        }

        public async Task<IEnumerable<PaymentAgingDto>> GetPaymentAgingAsync(DateTime asOfDate)
        {
            var result = await _reportingRepository.GetPaymentAgingAsync(asOfDate);
            var dto = _mapper.Map<IEnumerable<PaymentAgingDto>>(result);
            return dto;
        }

        public async Task<IEnumerable<RevenueByMonthDto>> GetRevenueByMonthAsync(DateTime from, DateTime to)
        {
            var result = await _reportingRepository.GetRevenueByMonthAsync(from, to);
            var dto = _mapper.Map<IEnumerable<RevenueByMonthDto>>(result);
            return dto;
        }

        public async Task<IEnumerable<ServiceCategoryDto>> GetRevenueByServiceCategoryAsync(DateTime from, DateTime to)
        {
            var result = await _reportingRepository.GetRevenueByServiceCategoryAsync(from, to);
            var dto = _mapper.Map<IEnumerable<ServiceCategoryDto>>(result);
            return dto;
        }

        public async Task<IEnumerable<SupplierSpendDto>> GetSupplierSpendAsync(DateTime from, DateTime to)
        {
            var result = await _reportingRepository.GetSupplierSpendAsync(from, to);
            var dto = _mapper.Map<IEnumerable<SupplierSpendDto>>(result);
            return dto;
        }

        public async Task<IEnumerable<PartConsumptionDto>> GetTopConsumedPartsAsync(DateTime from, DateTime to, int top = 10)
        {
            var result = await _reportingRepository.GetTopConsumedPartsAsync(from, to, top);
            var dto = _mapper.Map<IEnumerable<PartConsumptionDto>>(result);
            return dto;
        }

        public async Task<IEnumerable<CustomerInsightDto>> GetTopCustomersAsync(DateTime from, DateTime to, int top = 10)
        {
            var result = await _reportingRepository.GetTopCustomersAsync(from, to, top);
            var dto = _mapper.Map<IEnumerable<CustomerInsightDto>>(result);
            return dto;
        }

        public async Task<IEnumerable<VehicleInsightDto>> GetTopVehiclesAsync(DateTime from, DateTime to, int top = 10)
        {
            var result = await _reportingRepository.GetTopVehiclesAsync(from, to, top);
            var dto = _mapper.Map<IEnumerable<VehicleInsightDto>>(result);
            return dto;
        }

        public async Task<WorkOrderSummaryDto> GetWorkOrderSummaryAsync(DateTime from, DateTime to)
        {
            var result = await _reportingRepository.GetWorkOrderSummaryAsync(from, to);
            var dto = _mapper.Map<WorkOrderSummaryDto>(result);
            return dto;
        }
    }
}
