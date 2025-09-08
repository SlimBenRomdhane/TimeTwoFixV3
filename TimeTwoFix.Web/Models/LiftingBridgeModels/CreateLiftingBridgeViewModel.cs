using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.LiftingBridgeModels
{
    public class CreateLiftingBridgeViewModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateOnly InstallationDate { get; set; }
        public string Status { get; set; }

        [Range(1, 10000)]
        public int LoadCapacity { get; set; } // in Kilograms

        public string Type { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}