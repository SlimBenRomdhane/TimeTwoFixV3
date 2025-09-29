namespace TimeTwoFix.Web.Models.ReportingModels
{
    public class SupplierSpendViewModel
    {
        public int ProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public decimal TotalSpend { get; set; }       // raw value, formatting handled in view
        public int DeliveriesCount { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? SupplierName { get; set; }  // Alias for ProviderName for compatibility
        public string? SupplierType { get; set; }
    }
}
