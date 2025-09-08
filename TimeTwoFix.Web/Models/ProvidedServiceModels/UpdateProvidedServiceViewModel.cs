using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.ProvidedServiceModels
{
    public class UpdateProvidedServiceViewModel
    {
        [Required]
        public int CategoryId { get; set; }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public int EstimatedTime { get; set; }

        [Required]
        public decimal PricePerHour { get; set; }

        [MaxLength(255)]
        public string? Notes { get; set; }
    }
}