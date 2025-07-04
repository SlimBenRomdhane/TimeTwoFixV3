using Microsoft.AspNetCore.Identity;
using TimeTwoFix.Application.UserServices.Dtos.Roles;
using TimeTwoFix.Application.UserServices.Dtos.Users;

namespace TimeTwoFix.Application.UserServices.Interfaces
{
    public interface IUserService
    {
        /// User management
        Task<IdentityResult> CreateUserAsync(CreateUserDto createUserDto);
        Task<ReadUserDto?> GetUserByEmailAsync(string email);
        Task<IEnumerable<ReadUserDto>> GetAllApplicationUsers();
        Task<IdentityResult> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<IdentityResult> DeleteUserAsync(int userId);
        Task<bool> CheckPasswordAsync(ReadUserDto readUserDto, string password);
        Task<IList<string>> GetUserRolesAsync(ReadUserDto readUserDto);
        Task<IdentityResult> AddOrUpdateUserToRoleAsync(ReadUserDto readUserDto, ReadRoleDto readRoleDto);
        Task<SignInResult> SignInAsync(string email, string password, bool isPersistent);
        Task SignOutAsync();
        Task<IEnumerable<ReadUserDto>> GetUsersByMultipleParam(UserFilterDto userFilterDto);
    }
}