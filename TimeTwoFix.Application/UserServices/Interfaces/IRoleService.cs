using Microsoft.AspNetCore.Identity;
using TimeTwoFix.Application.UserServices.Dtos.Roles;

namespace TimeTwoFix.Application.UserServices.Interfaces
{
    public interface IRoleService
    {
        Task<bool> RoleExistsAsync(string roleName);

        Task<IdentityResult> CreateRoleAsync(CreateRoleDto role);

        Task<IdentityResult> DeleteRoleAsync(string roleName);

        Task<IdentityResult> UpdateRoleNameAsync(UpdateRoleDto role);

        Task<IEnumerable<ReadRoleDto>> GetAllRolesAsync();

        Task<ReadRoleDto?> GetRoleByNameAsync(string roleName);
    }
}