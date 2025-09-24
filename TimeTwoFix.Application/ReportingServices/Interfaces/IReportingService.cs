using TimeTwoFix.Application.ReportingServices.Dtos;

namespace TimeTwoFix.Application.ReportingServices.Interfaces
{
    public interface IReportingService
    {
        Task<WorkOrderSummaryDto> GetWorkOrderSummaryAsync(DateTime from, DateTime to);
        Task<IEnumerable<RevenueByMonthDto>> GetRevenueByMonthAsync(DateTime from, DateTime to);
        Task<IEnumerable<MechanicPerformanceDto>> GetMechanicPerformanceAsync(DateTime from, DateTime to);
        Task<IEnumerable<CustomerInsightDto>> GetTopCustomersAsync(DateTime from, DateTime to, int top = 10);
        Task<IEnumerable<PaymentAgingDto>> GetPaymentAgingAsync(DateTime asOfDate);
        Task<IEnumerable<ServiceCategoryDto>> GetRevenueByServiceCategoryAsync(DateTime from, DateTime to);
        Task<IEnumerable<VehicleInsightDto>> GetTopVehiclesAsync(DateTime from, DateTime to, int top = 10);
        Task<IEnumerable<PartConsumptionDto>> GetTopConsumedPartsAsync(DateTime from, DateTime to, int top = 10);
        Task<IEnumerable<SupplierSpendDto>> GetSupplierSpendAsync(DateTime from, DateTime to);
        Task<IEnumerable<PauseAnalysisDto>> GetPauseAnalysisAsync(DateTime from, DateTime to);
    }
}
