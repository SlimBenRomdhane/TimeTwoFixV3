using Microsoft.AspNetCore.Identity;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.UserServices.Dtos.Roles;
using TimeTwoFix.Application.UserServices.Dtos.Users;
using TimeTwoFix.Core.Entities.UserManagement;

namespace TimeTwoFix.Application.UserServices.Interfaces
{
    public interface IUserService
    {
        // Role Management
        //Task<IdentityResult> CreateRoleAsync(ApplicationRole role);
        //Task<IdentityResult> UpdateRoleAsync(ApplicationRole role);
        //Task<IdentityResult> DeleteRoleAsync(ApplicationRole role);
        //Task<ApplicationRole> GetRoleByIdAsync(string roleId);
        //Task<ApplicationRole> GetRoleByNameAsync(string roleName);


        Task<IEnumerable<ReadUserDto>> GetUsersByMultipleParam(UserFilterDto userFilterDto);

        Task<IEnumerable<ReadUserDto>> GetAllApplicationUsers();

        Task<IEnumerable<ReadRoleDto>> GetAllApplicationRoles();
    }
}