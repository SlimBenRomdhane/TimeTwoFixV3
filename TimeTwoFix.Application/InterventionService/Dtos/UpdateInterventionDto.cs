namespace TimeTwoFix.Application.InterventionService.Dtos
{
    public class UpdateInterventionDto
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int MechanicId { get; set; }
        public int ServiceId { get; set; }
        public int LiftingBridgeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? ActualTimeSpent { get; set; }
        public decimal? InterventionPrice { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
    }
}
