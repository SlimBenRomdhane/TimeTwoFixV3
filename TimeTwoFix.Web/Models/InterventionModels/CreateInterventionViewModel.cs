using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.InterventionModels
{
    public class CreateInterventionViewModel
    {
        [Required]
        public int WorkOrderId { get; set; }
        [Required]
        public int MechanicId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public int LiftingBridgeId { get; set; }
        public DateTime StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        //public int ActualTimeSpent { get; set; }
        public decimal InterventionPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
    }
}
