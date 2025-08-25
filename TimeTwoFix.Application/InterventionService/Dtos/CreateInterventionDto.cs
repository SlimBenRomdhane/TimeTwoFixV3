namespace TimeTwoFix.Application.InterventionService.Dtos
{
    public class CreateInterventionDto
    {

        public int WorkOrderId { get; set; }
        public int MechanicId { get; set; }
        public int ServiceId { get; set; }
        public int LiftingBridgeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? ActualTimeSpent { get; set; }
        public decimal? InterventionPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }

    }
}
