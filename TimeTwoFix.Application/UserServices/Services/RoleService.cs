using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Application.UserServices.Dtos.Roles;
using TimeTwoFix.Application.UserServices.Interfaces;
using TimeTwoFix.Core.Entities.UserManagement;

namespace TimeTwoFix.Application.UserServices.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<ApplicationRole> roleManage, IMapper mapper)
        {
            _roleManager = roleManage;
            _mapper = mapper;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            var exists = await _roleManager.RoleExistsAsync(roleName);
            return exists;
        }

        public async Task<ReadRoleDto?> GetRoleByNameAsync(string roleName)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                return null;
            }
            var roleDto = _mapper.Map<ReadRoleDto>(role);
            return roleDto;
        }

        public async Task<IdentityResult> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            if (createRoleDto == null)
            {
                throw new ArgumentNullException(nameof(createRoleDto));
            }

            // Check if the role already exists
            if (await RoleExistsAsync(createRoleDto.RoleName))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role already exists" });
            }
            // Create a new role

            var role = new ApplicationRole
            {
                Name = createRoleDto.RoleName,
                Description = createRoleDto.Description,
                IsActive = createRoleDto.IsActive
            };
            var result = await _roleManager.CreateAsync(role);
            return result;
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleName)
        {
            var roleDto = await GetRoleByNameAsync(roleName);
            if (roleDto == null)
                return IdentityResult.Failed(new IdentityError { Description = "Role not found" });

            var actualRole = await _roleManager.FindByNameAsync(roleDto.Name);
            if (actualRole == null)
                return IdentityResult.Failed(new IdentityError { Description = "Role not found" });
            var result = await _roleManager.DeleteAsync(actualRole);
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role deletion failed" });
            }
        }

        public async Task<IEnumerable<ReadRoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var res = _mapper.Map<IEnumerable<ReadRoleDto>>(roles);
            return res;
        }

        public async Task<IdentityResult> UpdateRoleNameAsync(UpdateRoleDto updateRoleDto)
        {
            var role = await GetRoleByNameAsync(updateRoleDto.RoleName);
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            var actualRole = _roleManager.Roles.FirstOrDefault(r => r.Name == updateRoleDto.RoleName);
            if (actualRole == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role not found" });
            }
            actualRole.Name = updateRoleDto.RoleName;
            actualRole.Description = updateRoleDto.Description;
            actualRole.IsActive = updateRoleDto.IsActive;
            var result = await _roleManager.UpdateAsync(actualRole);
            return result;
        }
    }
}