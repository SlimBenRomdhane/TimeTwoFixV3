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
        [Range(0.01, double.MaxValue, ErrorMessage = "Price per hour must be greater than zero.")]
        public decimal PricePerHour { get; set; }
        public string? Notes { get; set; }


    }
}
