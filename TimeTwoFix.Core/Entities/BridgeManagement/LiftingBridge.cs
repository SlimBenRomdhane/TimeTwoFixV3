using System.ComponentModel.DataAnnotations;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.WorkOrderManagement;

namespace TimeTwoFix.Core.Entities.BridgeManagement
{
    public class LiftingBridge : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Location { get; set; }

        public DateOnly InstallationDate { get; set; }

        [MaxLength(50)]
        public required string Status { get; set; }

        public int LoadCapacity { get; set; } // in tons

        [MaxLength(50)]
        public string Type { get; set; }

        public ICollection<Intervention> Interventions { get; set; }
    }
}