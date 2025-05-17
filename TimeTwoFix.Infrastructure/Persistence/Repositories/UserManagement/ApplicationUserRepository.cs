using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.UserManagement;
using TimeTwoFix.Core.Interfaces.Repositories.IdentityManagement;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.UserManagement
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserRepository(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> CreateRoleAsync(ApplicationRole role)
        {
            var result = await _roleManager.CreateAsync(role);
            return result;
        }

        public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result;
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByIdAsync(email);
            return user;
        }

        public async Task<List<string>> GetUserRolesAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<ApplicationUser?> GetUsersByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<bool> RoleExistsAsync(string role)
        {
            var result = await _roleManager.RoleExistsAsync(role);
            return result;
        }

        public async Task<SignInResult> SignInAsync(ApplicationUser user, string password, bool isPersistent)
        {
            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure: false);
            return result;
        }

        public async Task<bool> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<IEnumerable<ApplicationUser?>> GetAllUsers()
        {
            var applicationUsers = await _userManager.Users.ToListAsync();
            return applicationUsers;
        }

        public async Task<IEnumerable<ApplicationRole?>> GetAllRoles()
        {
            var applicationRoles = await _roleManager.Roles.ToListAsync();
            return applicationRoles;
        }
    }
}