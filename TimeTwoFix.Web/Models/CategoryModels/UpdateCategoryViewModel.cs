using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.CategoryModels
{
    public class UpdateCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Description { get; set; }
        [MaxLength(255)]
        public string? Notes { get; set; }
    }
}
