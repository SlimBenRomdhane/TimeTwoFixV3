using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.ProviderModels
{
    public class UpdateProviderViewModel
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string ContactEmail { get; set; }
        [Required]
        public required string MobileContactPhone { get; set; }
        public string? LandContactPhone { get; set; }
        public string? Fax { get; set; }
        public string? Address { get; set; }
        public string? RIB { get; set; }
        public string? FiscalId { get; set; }
    }
}
