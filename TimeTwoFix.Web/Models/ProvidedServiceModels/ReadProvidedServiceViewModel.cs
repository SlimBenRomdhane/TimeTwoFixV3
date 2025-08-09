using System.ComponentModel.DataAnnotations;
using TimeTwoFix.Web.Models.CategoryModels;

namespace TimeTwoFix.Web.Models.ProvidedServiceModels
{
    public class ReadProvidedServiceViewModel
    {
        public ReadCategoryViewModel ReadCategoryViewModel { get; set; }
        public int CategoryId { get; set; }
        public int Id { get; set; }
        [Display(Name = "Service Name")]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int EstimatedTime { get; set; }
        public decimal PricePerHour { get; set; }
        public string? Notes { get; set; }


    }
}
