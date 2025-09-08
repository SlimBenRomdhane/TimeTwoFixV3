namespace TimeTwoFix.Application.InterventionService.Dtos
{
    public class DeleteInterventionDto
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int MechanicId { get; set; }
        public int ServiceId { get; set; }
        public int LiftingBridgeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? ActualTimeSpent { get; set; }
        public decimal InterventionPrice { get; set; }

        public DateTime DetetedAy { get; set; } = DateTime.Now;
        public string? DeletedBy { get; set; }
    }
}