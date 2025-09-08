using System.ComponentModel.DataAnnotations;
using TimeTwoFix.Web.Models.CategoryModels;

namespace TimeTwoFix.Web.Models.ProvidedServiceModels
{
    public class DeleteProvidedServiceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Service Name")]
        public string Name { get; set; }

        public string Description { get; set; }
        public int EstimatedTime { get; set; }
        public decimal PricePerHour { get; set; }
        public int CategoryId { get; set; }
        public DeleteCategoryViewModel DeleteCategoryViewModel { get; set; }
        public string Notes { get; set; }
    }
}