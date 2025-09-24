namespace TimeTwoFix.Core.Interfaces.Repositories.ReportingModels
{
    public class PauseAnalysisResult
    {
        public string Reason { get; set; }
        public int Occurrences { get; set; }
        public double TotalHoursLost { get; set; }
        public double AveragePauseMinutes { get; set; }
    }

}