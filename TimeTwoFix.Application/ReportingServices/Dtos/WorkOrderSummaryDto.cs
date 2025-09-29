namespace TimeTwoFix.Application.ReportingServices.Dtos
{

    public class WorkOrderSummaryDto
    {
        public int TotalCreated { get; set; }
        public int TotalClosed { get; set; }
        public double AverageDurationHours { get; set; }
        public decimal AverageRevenue { get; set; }
        public int PaidCount { get; set; }
        public int UnpaidCount { get; set; }
    }


}
