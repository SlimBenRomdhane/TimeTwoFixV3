using System.ComponentModel.DataAnnotations;
using TimeTwoFix.Core.Common;

namespace TimeTwoFix.Core.Entities.SparePartManagement
{
    public class SparePartCategory : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        public ICollection<SparePart>? SpareParts { get; set; }
    }
}