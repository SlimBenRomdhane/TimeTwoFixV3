namespace TimeTwoFix.Application.PauseRecordService.Dtos
{
    public class UpdatePauseRecordDto
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime? EndTime { get; set; } = DateTime.Now;
        public int InterventionId { get; set; }
    }
}
