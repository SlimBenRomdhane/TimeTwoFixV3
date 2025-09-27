namespace TimeTwoFix.Web.Models.ReportingModels
{
    public class CustomerInsightViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int TotalVisits { get; set; }
        public decimal TotalSpend { get; set; }   // e.g. "$1,250.00"
        public bool IsRepeatCustomer { get; set; }
        public decimal AverageInvoice { get; set; }  // e.g. "$312.50"
    }
}
