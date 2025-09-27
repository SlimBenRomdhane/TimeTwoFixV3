namespace TimeTwoFix.Web.Models.ReportingModels
{
    public class MechanicPerformanceViewModel
    {
        public int MechanicId { get; set; }
        public string MechanicName { get; set; } = string.Empty;
        public int JobsCompleted { get; set; }
        public double AverageCompletionHours { get; set; }
        public decimal TotalRevenue { get; set; }

    }
}
