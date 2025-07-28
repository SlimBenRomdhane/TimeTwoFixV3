using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.ClientModels
{
    public class CreateClientViewModel
    {
        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }

        [MaxLength(50)]
        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(255)]
        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }
    }
}