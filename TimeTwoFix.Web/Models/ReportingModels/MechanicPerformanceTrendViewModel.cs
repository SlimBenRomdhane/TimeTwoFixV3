namespace TimeTwoFix.Web.Models.ReportingModels
{
    public class MechanicPerformanceTrendViewModel
    {
        public string MechanicName { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty;
        public int JobsCompleted { get; set; }
        public decimal AverageCompletionHours { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
