namespace TimeTwoFix.Application.PauseRecordService.Dtos
{
    public class ReadPauseRecordDto
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int InterventionId { get; set; }
        public TimeSpan? PauseDuration { get; set; }
    }
}