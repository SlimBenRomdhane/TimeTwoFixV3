using System.ComponentModel.DataAnnotations;
using TimeTwoFix.Core.Common;

namespace TimeTwoFix.Core.Entities.SparePartManagement
{
    public class Provider : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public required string ContactEmail { get; set; }
        [Required]
        [MaxLength(20)]
        public required string MobileContactPhone { get; set; }
        public string LandContactPhone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string RIB { get; set; }
        public string FiscalId { get; set; }
        public ICollection<ProviderSparePart>? ProviderSpareParts { get; set; }

    }
}
