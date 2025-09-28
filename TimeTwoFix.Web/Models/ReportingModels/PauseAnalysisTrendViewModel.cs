namespace TimeTwoFix.Web.Models.ReportingModels
{
    public class PauseAnalysisTrendViewModel
    {
        public string Reason { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty; // e.g. "Sep 2025"
        public double HoursLost { get; set; }
    }
}
