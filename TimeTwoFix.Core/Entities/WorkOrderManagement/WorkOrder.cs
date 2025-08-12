using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.VehicleManagement;

namespace TimeTwoFix.Core.Entities.WorkOrderManagement
{
    public class WorkOrder : BaseEntity
    {
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        [Required]
        public int Mileage { get; set; }

        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal TolalLaborCost { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public Vehicle Vehicle { get; set; }
        public ICollection<Intervention> Interventions { get; set; }
    }
}