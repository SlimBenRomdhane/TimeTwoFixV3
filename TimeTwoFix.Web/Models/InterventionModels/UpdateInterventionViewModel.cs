using System.ComponentModel;

namespace TimeTwoFix.Web.Models.InterventionModels
{
    public class UpdateInterventionViewModel
    {
        public int WorkOrderId { get; set; }

        public int MechanicId { get; set; }

        public int ServiceId { get; set; }

        public int LiftingBridgeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ReadOnly(true)]
        public int ActualTimeSpent { get; set; }
        [ReadOnly(true)]
        public decimal InterventionPrice { get; set; }

    }
}
