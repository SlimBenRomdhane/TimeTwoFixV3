using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.SparePartModels
{
    public class CreateSparePartViewModel
    {
        public int SparePartCategoryId { get; set; }
        [Required]
        [MaxLength(50)]
        public string PartCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

    }
}