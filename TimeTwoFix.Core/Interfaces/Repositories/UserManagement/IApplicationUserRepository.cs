using Microsoft.AspNetCore.Identity;
using TimeTwoFix.Core.Entities.UserManagement;

namespace TimeTwoFix.Core.Interfaces.Repositories.IdentityManagement
{
    public interface IApplicationUserRepository
    {
        //UserManager and RoleManager Methods
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);

        Task<IdentityResult> CreateRoleAsync(ApplicationRole role);

        Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role);

        Task<ApplicationUser?> GetUserByEmailAsync(string email);

        Task<ApplicationUser?> GetUsersByUserNameAsync(string userName);

        Task<IEnumerable<ApplicationUser?>> GetAllUsers();

        Task<IEnumerable<ApplicationRole?>> GetAllRoles();

        Task<List<string>> GetUserRolesAsync(ApplicationUser user);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        Task<bool> RoleExistsAsync(string role);

        //SignInManager Methods
        Task<SignInResult> SignInAsync(ApplicationUser user, string password, bool isPersistent);

        Task<bool> SignOutAsync();
    }
}