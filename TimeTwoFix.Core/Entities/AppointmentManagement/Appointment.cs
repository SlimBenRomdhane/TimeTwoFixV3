using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.VehicleManagement;

namespace TimeTwoFix.Core.Entities.AppointmentManagement
{
    public class Appointment : BaseEntity
    {
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        public DateOnly AppointmentDate { get; set; }

        [Required]
        public TimeOnly AppointmentTime { get; set; }

        [Required]
        [MaxLength(50)]
        public required string ContactName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string ContactPhone { get; set; }

        [MaxLength(50)]
        public string Urgency { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}