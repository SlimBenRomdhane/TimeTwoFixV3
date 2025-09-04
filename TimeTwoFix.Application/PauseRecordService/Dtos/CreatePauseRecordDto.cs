namespace TimeTwoFix.Application.PauseRecordService.Dtos
{
    public class CreatePauseRecordDto
    {
        public string Reason { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public int InterventionId { get; set; }

    }
}
