using System.ComponentModel.DataAnnotations;
using TimeTwoFix.Core.Entities.SparePartManagement;

namespace TimeTwoFix.Web.Models.SparePartModels
{
    public class CreateSparePartViewModel
    {
        [Required]
        [MaxLength(50)]
        public string PartCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int QuantityInStock { get; set; }

    }
}
