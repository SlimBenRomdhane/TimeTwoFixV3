using System.ComponentModel.DataAnnotations;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.VehicleManagement;

namespace TimeTwoFix.Core.Entities.ClientManagement
{
    public class Client : BaseEntity
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
        public string Email { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}