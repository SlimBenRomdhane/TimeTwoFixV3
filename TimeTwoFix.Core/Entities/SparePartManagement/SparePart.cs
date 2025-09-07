using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTwoFix.Core.Common;

namespace TimeTwoFix.Core.Entities.SparePartManagement
{
    [Index(nameof(PartCode), IsUnique = true)]
    public class SparePart : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        [ForeignKey("SparePartCategory")]
        public int SparePartCategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string PartCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int QuantityInStock { get; set; }

        // Navigation properties pour le destockage des pièces
        public ICollection<InterventionSparePart> InterventionSpareParts { get; set; }
        // Navigation properties pour l'alimentation des pièces
        public ICollection<ProviderSparePart> ProviderSpareParts { get; set; }
        public SparePartCategory SparePartCategory { get; set; }
    }
}