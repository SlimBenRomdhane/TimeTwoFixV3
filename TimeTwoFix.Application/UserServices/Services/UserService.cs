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
        private readonly IRoleService _roleService;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager, IMapper mapper, IRoleService roleService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleService = roleService;
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

        public async Task<IdentityResult> CreateUserAsync(CreateUserDto createUserDto)
        {
            // Check if the user already exists
            var existingUser = await _userManager.FindByEmailAsync(createUserDto.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }

            ApplicationUser user;
            if (createUserDto.UserType == "Mechanic")
            {
                user = _mapper.Map<Mechanic>(createUserDto);
            }
            else if (createUserDto.UserType == "FrontDeskAssistant")
            {
                user = _mapper.Map<FrontDeskAssistant>(createUserDto);
            }
            else if (createUserDto.UserType == "WareHouseManager")
            {
                user = _mapper.Map<WareHouseManager>(createUserDto);
            }
            else if (createUserDto.UserType == "WorkShopManager")
            {
                user = _mapper.Map<WorkshopManager>(createUserDto);
            }
            else if (createUserDto.UserType == "GeneralManager")
            {
                user = _mapper.Map<GeneralManager>(createUserDto);
            }
            else
            {
                throw new Exception("User type not found");
            }
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            var res = await _userManager.CreateAsync(user, createUserDto.Password);
            return res;
        }

        public async Task<ReadUserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var userDto = _mapper.Map<ReadUserDto>(user);
                return userDto;
            }
            else
            {
                throw new KeyNotFoundException($"No user with {email} address exists.");
            }
        }

        public async Task<IdentityResult> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByEmailAsync(updateUserDto.Email);
            if (user != null)
            {
                user.UserName = updateUserDto.UserName;
                user.FirstName = updateUserDto.FirstName;
                user.LastName = updateUserDto.LastName;
                user.Address = updateUserDto.Address;
                user.City = updateUserDto.City;
                user.ZipCode = updateUserDto.ZipCode;
                user.PhoneNumber = updateUserDto.PhoneNumber;
                user.Email = updateUserDto.Email;
                user.Status = updateUserDto.Status;

                if (!string.IsNullOrEmpty(updateUserDto.Password))
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, updateUserDto.Password);
                }
                user.ImageUrl = updateUserDto.ImageURL;
                user.HireDate = updateUserDto.HireDate;
                user.YearsOfExperience = updateUserDto.YearsOfExperience;
                user.LastEmployer = updateUserDto.LastEmployer;
                user.Status = updateUserDto.Status;

                if (user is Mechanic mechanic)
                {
                    mechanic.ToolBoxNumber = updateUserDto.ToolBoxNumber;
                    mechanic.AbleToShift = updateUserDto.AbleToShift;
                    mechanic.Specialization = updateUserDto.Specialization;
                }
                else if (user is FrontDeskAssistant assistant)
                {
                    assistant.WorkStationNumber = updateUserDto.WorkStationNumber;
                    assistant.PhoneExtension = updateUserDto.PhoneExtension;
                    assistant.SpokenLanguage = updateUserDto.SpokenLanguage;
                    assistant.BusinessKnowledge = updateUserDto.BusinessKnowledge;
                }
                else if (user is GeneralManager generalManager)
                {
                    generalManager.OfficeNumber = updateUserDto.OfficeNumber;
                    generalManager.YearsInManagement = updateUserDto.YearsInManagement;
                }
                else if (user is WareHouseManager warehouseManager)
                {
                    warehouseManager.WarehouseName = updateUserDto.WarehouseName;
                    warehouseManager.WarehouseLocation = updateUserDto.WarehouseLocation;
                    warehouseManager.AbleToShiftWareHouse = updateUserDto.AbleToShiftWareHouse;
                }
                else if (user is WorkshopManager workshopManager)
                {
                    workshopManager.TeamSize = updateUserDto.TeamSize;
                }

                // Update specific properties based on the role
                var res = await _userManager.UpdateAsync(user);
                return res;
            }
            else
            {
                throw new KeyNotFoundException($"No user with {updateUserDto.Email} address exists.");
            }
        }

        public async Task<IdentityResult> DeleteUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result;
            }
            else
            {
                throw new KeyNotFoundException($"No user with {userId} address exists.");
            }
        }

        public async Task<bool> CheckPasswordAsync(ReadUserDto readUserDto, string password)
        {
            var actualUser = await _userManager.FindByEmailAsync(readUserDto.Email);
            if (actualUser != null)
            {
                var result = await _userManager.CheckPasswordAsync(actualUser, password);
                return result;
            }
            else
            {
                throw new KeyNotFoundException($"No user with {readUserDto.Email} address exists.");
            }
        }

        public async Task<IList<string>> GetUserRolesAsync(ReadUserDto readUserDto)
        {
            var user = await _userManager.FindByEmailAsync(readUserDto.Email);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                return roles;
            }
            else
            {
                throw new KeyNotFoundException($"No user with {readUserDto.Email} address exists.");
            }
        }

        public async Task<IdentityResult> AddOrUpdateUserToRoleAsync(ReadUserDto readUserDto, ReadRoleDto readRoleDto)
        {
            var user = await _userManager.FindByEmailAsync(readUserDto.Email);
            if (user == null)
            {
                throw new KeyNotFoundException($"No user with {readUserDto.Email} address exists.");
            }

            //Get the role
            var currentRole = await _userManager.GetRolesAsync(user);
            if (currentRole.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, currentRole);
            }
            var result = await _userManager.AddToRoleAsync(user, readRoleDto.Name);
            return result;
        }

        public Task<SignInResult> SignInAsync(string email, string password, bool isPersistent)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == email);
            if (user != null)
            {
                return _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure: false);
            }
            else
            {
                throw new KeyNotFoundException($"No user with {email} address exists.");
            }
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }
    }
}