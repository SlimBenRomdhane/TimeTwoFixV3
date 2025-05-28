using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.UserManagement;

namespace TimeTwoFix.Infrastructure.Persistence.DbInit
{
    public static class AdminUserInit
    {

        public static void SeedUser(this ModelBuilder modelBuilder)
        {

            var adminUser = new GeneralManager
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                Address = "admin",
                City = "admin",
                FirstName = "admin",
                LastName = "admin",
                ImageUrl = "admin",
                LastEmployer = "admin",
                UserType = "GeneralManager",
                Status = "Active",
                SecurityStamp = new Guid().ToString(),
                OfficeNumber= "manager",
                YearsInManagement=99,
                PhoneNumber="99999999",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Hash the password
            var passwordHasher = new PasswordHasher<GeneralManager>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Capgemini_2025");

            modelBuilder.Entity<GeneralManager>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                UserId = 1,
                RoleId = 5 // Assuming the role ID for Admin is 1
            });

        }
    }
}