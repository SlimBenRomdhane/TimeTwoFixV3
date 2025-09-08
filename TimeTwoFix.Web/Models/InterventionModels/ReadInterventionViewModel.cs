using TimeTwoFix.Application.Base;
using TimeTwoFix.Web.Models.LiftingBridgeModels;
using TimeTwoFix.Web.Models.ProvidedServiceModels;
using TimeTwoFix.Web.Models.UserModels;

namespace TimeTwoFix.Web.Models.InterventionModels
{
    public class ReadInterventionViewModel : AuditClass
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int MechanicId { get; set; }
        public ReadUserViewModel UserViewModel { get; set; }
        public int ServiceId { get; set; }
        public ReadProvidedServiceViewModel ProvidedService { get; set; }
        public int LiftingBridgeId { get; set; }
        public ReadLiftingBridgeViewModel LiftingBridge { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan ActualTimeSpent { get; set; }
        public decimal InterventionPrice { get; set; }
    }
}