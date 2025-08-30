using TimeTwoFix.Application.Base;

namespace TimeTwoFix.Web.Models.InterventionModels
{
    public class ReadInterventionViewModel : AuditClass
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int MechanicId { get; set; }
        public int ServiceId { get; set; }
        public int LiftingBridgeId { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan ActualTimeSpent { get; set; }
        public decimal InterventionPrice { get; set; }


    }
}
