namespace TimeTwoFix.Web.Models.ReportingModels
{
    public class SupplierSpendViewModel
    {
        public int ProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public decimal TotalSpend { get; set; }       // raw value, formatting handled in view
        public int DeliveriesCount { get; set; }
    }
}
