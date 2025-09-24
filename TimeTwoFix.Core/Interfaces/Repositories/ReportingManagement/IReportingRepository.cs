using TimeTwoFix.Core.Interfaces.Repositories.ReportingModels;

namespace TimeTwoFix.Core.Interfaces.Repositories.ReportingManagement
{
    public interface IReportingRepository
    {
        Task<WorkOrderSummaryResult> GetWorkOrderSummaryAsync(DateTime from, DateTime to);
        Task<IEnumerable<RevenueByMonthResult>> GetRevenueByMonthAsync(DateTime from, DateTime to);
        Task<IEnumerable<MechanicPerformanceResult>> GetMechanicPerformanceAsync(DateTime from, DateTime to);
        Task<IEnumerable<CustomerInsightResult>> GetTopCustomersAsync(DateTime from, DateTime to, int top = 10);
        Task<IEnumerable<PaymentAgingResult>> GetPaymentAgingAsync(DateTime asOfDate);
        Task<IEnumerable<ServiceCategoryResult>> GetRevenueByServiceCategoryAsync(DateTime from, DateTime to);
        Task<IEnumerable<VehicleInsightResult>> GetTopVehiclesAsync(DateTime from, DateTime to, int top = 10);
        Task<IEnumerable<PartConsumptionResult>> GetTopConsumedPartsAsync(DateTime from, DateTime to, int top = 10);
        Task<IEnumerable<SupplierSpendResult>> GetSupplierSpendAsync(DateTime from, DateTime to);
        Task<IEnumerable<PauseAnalysisResult>> GetPauseAnalysisAsync(DateTime from, DateTime to);
    }

}
