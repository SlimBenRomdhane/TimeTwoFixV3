using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.CategoryModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string? Notes { get; set; }
    }
}