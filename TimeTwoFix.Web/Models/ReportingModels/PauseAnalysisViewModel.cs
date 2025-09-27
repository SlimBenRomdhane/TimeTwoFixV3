namespace TimeTwoFix.Web.Models.ReportingModels
{
    public class PauseAnalysisViewModel
    {
        public string Reason { get; set; } = string.Empty;
        public int Occurrences { get; set; }
        public double TotalHoursLost { get; set; }
        public double AveragePauseMinutes { get; set; }
    }
}
