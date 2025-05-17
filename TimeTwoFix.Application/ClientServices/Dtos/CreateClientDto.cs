using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Application.ClientServices.Dtos
{
    public class CreateClientDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Notes { get; set; }
        public DateTime? CreatedAt { get; set; }

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        public CreateClientDto()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}