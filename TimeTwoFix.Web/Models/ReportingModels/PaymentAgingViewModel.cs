namespace TimeTwoFix.Web.Models.ReportingModels
{
    public class PaymentAgingViewModel
    {
        public string AgingBucket { get; set; } = string.Empty;       // e.g. "0–30 days"
        public decimal AmountDue { get; set; }
        public int WorkOrderId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public int DaysOutstanding { get; set; }
    }
}
