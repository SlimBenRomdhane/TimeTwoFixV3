using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.ProvidedServiceModels
{
    public class CreateProvidedServiceViewModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Service Name")]
        public required string Name { get; set; }

        [MaxLength(100)]
        public required string Description { get; set; }

        [Required]
        public int EstimatedTime { get; set; }

        [Required]
        public decimal PricePerHour { get; set; }
        public string? Notes { get; set; }


    }
}
