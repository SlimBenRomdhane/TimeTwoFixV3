using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTwoFix.Core.Entities.UserManagement
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [Range(1, 99999)]
        public int ZipCode { get; set; }
        public string? ImageUrl { get; set; }

        [Required]
        public DateOnly HireDate { get; set; }

        public decimal HourlyWage { get; set; }

        [Range(0, 50)]
        public int YearsOfExperience { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastEmployer { get; set; }

        //Indicates if the user is currently employed, on leave, or terminated...
        [Required]
        [MaxLength(50)]

        public string Status { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [NotMapped]
        public override string? NormalizedEmail { get; set; }
    }
}