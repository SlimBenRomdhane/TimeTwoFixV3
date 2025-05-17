using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Application.ClientServices.Dtos
{
    public class UpdateClientDto
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [MaxLength(50)]
        public string? UpdatedBy { get; set; }

        public UpdateClientDto()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}