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
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        public RoleService(RoleManager<ApplicationRole> roleManage, IMapper mapper)
        {
            _roleManager = roleManage;
            _mapper = mapper;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            var exists = await _roleManager.RoleExistsAsync(roleName);
            if (exists)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                throw new Exception("Role cannot be null");
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
            var createdRole = await _roleManager.CreateAsync(role);
            if (createdRole.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role creation failed" });
            }
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleName)
        {

            var roleDto = await GetRoleByNameAsync(roleName);
            if (roleDto == null)
                return IdentityResult.Failed(new IdentityError { Description = "Role not found" });

            var actualRole = await _roleManager.FindByNameAsync(roleDto.RoleName);
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
            if (roles == null)
            {
                throw new Exception("Roles not found");
            }
            var res = _mapper.Map<IEnumerable<ReadRoleDto>>(roles);
            return res;
        }

        public Task<IdentityResult> UpdateRoleNameAsync(UpdateRoleDto updateRoleDto)
        {
            var role = GetRoleByNameAsync(updateRoleDto.RoleName);
            if (role == null)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Role not found" }));
            }
            var actualRole = _roleManager.Roles.FirstOrDefault(r => r.Name == updateRoleDto.RoleName);
            if (actualRole == null)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Role not found" }));
            }
            actualRole.Name = updateRoleDto.RoleName;
            actualRole.Description = updateRoleDto.Description;
            actualRole.IsActive = updateRoleDto.IsActive;
            var result = _roleManager.UpdateAsync(actualRole);
            if (result.Result.Succeeded)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Role update failed" }));
            }

        }
    }
}
