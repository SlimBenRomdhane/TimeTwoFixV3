using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Application.UserServices.Dtos.Roles;
using TimeTwoFix.Application.UserServices.Dtos.Users;
using TimeTwoFix.Application.UserServices.Interfaces;
using TimeTwoFix.Core.Entities.UserManagement;

namespace TimeTwoFix.Application.UserServices.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;

        }

        public async Task<IEnumerable<ReadRoleDto>> GetAllApplicationRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (roles == null)
            {
                throw new Exception("Roles not found");
            }
            var res = _mapper.Map<IEnumerable<ReadRoleDto>>(roles);
            return res;
        }

        public async Task<IEnumerable<ReadUserDto>> GetAllApplicationUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null)
            {
                throw new Exception("Users not found");
            }
            var res = _mapper.Map<IEnumerable<ReadUserDto>>(users);
            return res;
        }

        public async Task<IEnumerable<ReadUserDto>> GetUsersByMultipleParam(UserFilterDto userFilterDto)
        {
            var query = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(userFilterDto.FirstName))
            {
                query = query.Where(u => u.FirstName.Contains(userFilterDto.FirstName));
            }
            if (!string.IsNullOrEmpty(userFilterDto.LastName))
            {
                query = query.Where(u => u.LastName.Contains(userFilterDto.LastName));
            }
            if (!string.IsNullOrEmpty(userFilterDto.Address))
            {
                query = query.Where(u => u.Address.Contains(userFilterDto.Address));
            }
            if (!string.IsNullOrEmpty(userFilterDto.City))
            {
                query = query.Where(u => u.City.Contains(userFilterDto.City));
            }
            if (userFilterDto.ZipCode != null)
            {
                query = query.Where(u => u.ZipCode == userFilterDto.ZipCode);
            }
            if (!string.IsNullOrEmpty(userFilterDto.PhoneNumber))
            {
                query = query.Where(u => u.PhoneNumber.Contains(userFilterDto.PhoneNumber));
            }
            if (!string.IsNullOrEmpty(userFilterDto.Email))
            {
                query = query.Where(u => u.Email.Contains(userFilterDto.Email));
            }
            if (userFilterDto.HireDate != null)
            {
                query = query.Where(u => u.HireDate == userFilterDto.HireDate);
            }
            var users = query.ToList();
            if (users == null)
            {
                throw new Exception("Users not found");
            }
            var res = _mapper.Map<IEnumerable<ReadUserDto>>(users);
            return res;
        }


    }
}