namespace TimeTwoFix.Core.Interfaces.Repositories.ReportingModels
{
    public class SupplierSpendResult
    {
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public decimal TotalSpend { get; set; } // Sum of purchases in period
        public int DeliveriesCount { get; set; } // Number of stock increases
    }

}