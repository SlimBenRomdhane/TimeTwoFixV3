using TimeTwoFix.Application.Base;

namespace TimeTwoFix.Application.InterventionService.Dtos
{
    public class ReadInterventionDto : AuditClass
    {
        public int WorkOrderId { get; set; }
        public int MechanicId { get; set; }
        public int ServiceId { get; set; }
        public int LiftingBridgeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ActualTimeSpent { get; set; }
        public decimal InterventionPrice { get; set; }
    }
}
