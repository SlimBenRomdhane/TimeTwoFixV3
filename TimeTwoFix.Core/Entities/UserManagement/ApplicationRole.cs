using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Core.Entities.UserManagement
{
    public class ApplicationRole : IdentityRole<int>
    {
        [MaxLength(100)]
        [Required]
        public string Description { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ApplicationRole()
        {
            CreatedAt = DateTime.UtcNow;
        }

        //public ApplicationRole(string roleName) : base(roleName)
        //{
        //}
    }
}