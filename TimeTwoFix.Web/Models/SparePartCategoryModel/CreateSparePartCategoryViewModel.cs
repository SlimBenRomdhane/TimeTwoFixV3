using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.SparePartCategoryModel
{
    public class CreateSparePartCategoryViewModel
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public required string Description { get; set; }

        [MaxLength(255)]
        public string? Notes { get; set; }
    }
}
