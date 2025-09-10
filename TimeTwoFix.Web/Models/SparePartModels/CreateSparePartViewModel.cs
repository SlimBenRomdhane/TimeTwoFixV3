using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.SparePartModels
{
    public class CreateSparePartViewModel
    {
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please select a category")]
        public int SparePartCategoryId { get; set; }

        [Display(Name = "Part Code")]
        [Required(ErrorMessage = "Part code is required")]
        [MaxLength(50, ErrorMessage = "Part code cannot exceed 50 characters")]
        public string PartCode { get; set; }

        [Display(Name = "Part Name")]
        [Required(ErrorMessage = "Part name is required")]
        [MaxLength(100, ErrorMessage = "Part name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        public string Description { get; set; }
        public List<CategoryDropdownItem> AvailableCategories { get; set; } = new();

    }
}