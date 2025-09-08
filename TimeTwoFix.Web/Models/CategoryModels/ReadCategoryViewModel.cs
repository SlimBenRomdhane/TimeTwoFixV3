using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.CategoryModels
{
    public class ReadCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Category Name")]
        public string? Name { get; set; }

        public string? Description { get; set; }
        public string? Notes { get; set; }
    }
}