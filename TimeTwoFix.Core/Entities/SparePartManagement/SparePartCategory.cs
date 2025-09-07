using System.ComponentModel.DataAnnotations;
using TimeTwoFix.Core.Common;

namespace TimeTwoFix.Core.Entities.SparePartManagement
{
    public class SparePartCategory : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public required string Description { get; set; }
        public ICollection<SparePart>? SpareParts { get; set; }
    }
}
