namespace TimeTwoFix.Web.Models.ReportingModels
{
    public class WorkOrderSummaryViewModel
    {
        public int TotalCreated { get; set; }
        public int TotalClosed { get; set; }
        public double AverageDurationHours { get; set; }
        public int PaidCount { get; set; }
        public int UnpaidCount { get; set; }
    }
}
