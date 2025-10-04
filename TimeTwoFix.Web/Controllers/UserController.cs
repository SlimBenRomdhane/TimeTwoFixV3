﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Core.Common.Constants;
using TimeTwoFix.Core.Entities.UserManagement;
using TimeTwoFix.Core.Interfaces;
using TimeTwoFix.Web.Models.RoleModels;
using TimeTwoFix.Web.Models.UserModels;
using TimeTwoFix.Application.UserServices.Dtos.Roles;
using TimeTwoFix.Application.UserServices.Dtos.Users;
using TimeTwoFix.Application.UserServices.Interfaces;

namespace TimeTwoFix.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper, IRoleService roleService, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        public IActionResult CreateRole()
        {
            return View();
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var check = await _roleService.RoleExistsAsync(createRoleViewModel.RoleName);
                if (!check)
                {
                    var roleName = new ApplicationRole
                    {
                        Name = createRoleViewModel.RoleName,
                        Description = createRoleViewModel.Description,
                        IsActive = createRoleViewModel.IsActive
                    };
                    var roleDto = _mapper.Map<CreateRoleDto>(createRoleViewModel);
                    var result = await _roleService.CreateRoleAsync(roleDto);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("RoleName", "This role already exists.");
                    return View(createRoleViewModel);
                }
            }
            return View();
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        public async Task<IActionResult> Index()
        {
            var usersDto = await _userService.GetAllApplicationUsers();
            var rolesDto = await _roleService.GetAllRolesAsync();
            var userViewModel = _mapper.Map<IEnumerable<ReadUserViewModel>>(usersDto).ToList();
            var roleViewModel = _mapper.Map<IEnumerable<ReadRoleViewModel>>(rolesDto).ToList();

            //var mechanics = users.OfType<Mechanic>().ToList();
            //var assistants = users.OfType<FrontDeskAssistant>().ToList();
            //var managers = users.OfType<GeneralManager>().ToList();
            //var warehouseManagers = users.OfType<WareHouseManager>().ToList();
            //var workshopManagers = users.OfType<WorkshopManager>().ToList();
            //var userView = _mapper.Map<IEnumerable<ReadUserViewModel>>(users);

            var viewModel = new Tuple<List<ReadUserViewModel>, List<ReadRoleViewModel>>(userViewModel, roleViewModel);
            return View(viewModel);
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        public IActionResult CreateMechanic()
        {
            return View();
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        [HttpPost]
        public async Task<IActionResult> CreateMechanic(CreateMechanicViewModel createMechanicViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createMechanicViewModel);
            }
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var mechanicDto = _mapper.Map<CreateUserDto>(createMechanicViewModel);
                    mechanicDto.UserName = createMechanicViewModel.Email;
                    mechanicDto.Status = "Active";
                    mechanicDto.UserType = "Mechanic";

                    var result = await _userService.CreateUserAsync(mechanicDto);

                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create user");
                    }

                    var createdUser = await _userService.GetUserByEmailAsync(mechanicDto.Email);
                    if (createdUser == null)
                    {
                        throw new Exception("Failed to retrieve created user");
                    }
                    var userAssignedRole = _mapper.Map<ReadUserDto>(createdUser);
                    var res = await _userService.AddOrUpdateUserToRoleAsync(userAssignedRole, new ReadRoleDto { Name = "Mechanic" });
                    if (!res.Succeeded)
                    {
                        throw new Exception("Failed to add user to role");
                    }
                    await transaction.CommitAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", $"An error occurred while creating the user: {ex.Message}");
                    return View(createMechanicViewModel);
                }
            }
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        public IActionResult CreateAssistant()
        {
            return View();
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        [HttpPost]
        public async Task<IActionResult> CreateAssistant(CreateFrontDeskAssistantViewModel createFrontDeskAssistantViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createFrontDeskAssistantViewModel);
            }
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var assistantDto = _mapper.Map<CreateUserDto>(createFrontDeskAssistantViewModel);
                    assistantDto.UserName = createFrontDeskAssistantViewModel.Email;
                    assistantDto.Status = "Active";
                    assistantDto.UserType = "FrontDeskAssistant";
                    var result = await _userService.CreateUserAsync(assistantDto);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create user");
                    }
                    var createdUser = await _userService.GetUserByEmailAsync(assistantDto.Email);
                    if (createdUser == null)
                    {
                        throw new Exception("Failed to retrieve created user");
                    }
                    var userAssignedRole = _mapper.Map<ReadUserDto>(createdUser);
                    var res = await _userService.AddOrUpdateUserToRoleAsync(userAssignedRole, new ReadRoleDto { Name = "FrontDeskAssistant" });
                    if (!res.Succeeded)
                    {
                        throw new Exception("Failed to add user to role");
                    }
                    await transaction.CommitAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", $"An error occurred while creating the user: {ex.Message}");
                    return View(createFrontDeskAssistantViewModel);
                }
            }
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        public IActionResult CreateWareHouseManager()
        {
            return View();
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        [HttpPost]
        public async Task<IActionResult> CreateWareHouseManager(CreateWareHouseManagerViewModel createWareHouseManagerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createWareHouseManagerViewModel);
            }
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var wareHouseManagerDto = _mapper.Map<CreateUserDto>(createWareHouseManagerViewModel);
                    wareHouseManagerDto.UserName = createWareHouseManagerViewModel.Email;
                    wareHouseManagerDto.Status = "Active";
                    wareHouseManagerDto.UserType = "WareHouseManager";
                    var result = await _userService.CreateUserAsync(wareHouseManagerDto);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create user");
                    }
                    var createdUser = await _userService.GetUserByEmailAsync(wareHouseManagerDto.Email);
                    if (createdUser == null)
                    {
                        throw new Exception("Failed to retrieve created user");
                    }
                    var userAssignedRole = _mapper.Map<ReadUserDto>(createdUser);
                    var res = await _userService.AddOrUpdateUserToRoleAsync(userAssignedRole, new ReadRoleDto { Name = "WareHouseManager" });
                    if (!res.Succeeded)
                    {
                        throw new Exception("Failed to add user to role");
                    }
                    await transaction.CommitAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", $"An error occurred while creating the user: {ex.Message}");
                    return View(createWareHouseManagerViewModel);
                }
            }
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        public IActionResult CreateWorkshopManager()
        {
            return View();
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        [HttpPost]
        public async Task<IActionResult> CreateWorkshopManager(CreateWorkshopManagerViewModel createWorkshopManagerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createWorkshopManagerViewModel);
            }
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var workShopManagerDto = _mapper.Map<CreateUserDto>(createWorkshopManagerViewModel);
                    workShopManagerDto.UserName = createWorkshopManagerViewModel.Email;
                    workShopManagerDto.Status = "Active";
                    workShopManagerDto.UserType = "WorkShopManager";
                    var result = await _userService.CreateUserAsync(workShopManagerDto);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create user");
                    }
                    var createdUser = await _userService.GetUserByEmailAsync(workShopManagerDto.Email);
                    if (createdUser == null)
                    {
                        throw new Exception("Failed to retrieve created user");
                    }
                    var userAssignedRole = _mapper.Map<ReadUserDto>(createdUser);
                    var res = await _userService.AddOrUpdateUserToRoleAsync(userAssignedRole, new ReadRoleDto { Name = "WorkShopManager" });
                    if (!res.Succeeded)
                    {
                        throw new Exception("Failed to add user to role");
                    }
                    await transaction.CommitAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", $"An error occurred while creating the user: {ex.Message}");
                    return View(createWorkshopManagerViewModel);
                }
            }
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        public IActionResult CreateGeneralManager()
        {
            return View();
        }

        [Authorize(Roles = RoleNames.GeneralManager)]
        [HttpPost]
        public async Task<IActionResult> CreateGeneralManager(CreateGeneralManagerViewModel createGeneralManagerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createGeneralManagerViewModel);
            }
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var generalManagerDto = _mapper.Map<CreateUserDto>(createGeneralManagerViewModel);
                    generalManagerDto.UserName = createGeneralManagerViewModel.Email;
                    generalManagerDto.Status = "Active";
                    generalManagerDto.UserType = "GeneralManager";
                    var result = await _userService.CreateUserAsync(generalManagerDto);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create user");
                    }
                    var createdUser = await _userService.GetUserByEmailAsync(generalManagerDto.Email);
                    if (createdUser == null)
                    {
                        throw new Exception("Failed to retrieve created user");
                    }
                    var userAssignedRole = _mapper.Map<ReadUserDto>(createdUser);
                    var res = await _userService.AddOrUpdateUserToRoleAsync(userAssignedRole, new ReadRoleDto { Name = "GeneralManager" });
                    if (!res.Succeeded)
                    {
                        throw new Exception("Failed to add user to role");
                    }
                    await transaction.CommitAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", $"An error occurred while creating the user: {ex.Message}");
                    return View(createGeneralManagerViewModel);
                }
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userService.SignInAsync(email, password, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }

            // Handle missing users without crashing
            ReadUserDto user = null;
            try
            {
                user = await _userService.GetUserByEmailAsync(email);
            }
            catch (KeyNotFoundException)
            {
                ModelState.AddModelError("", "User not found after login.");
                return View();
            }

            if (user == null)
            {
                ModelState.AddModelError("", "Unexpected error: User retrieval failed.");
                return View();
            }

            HttpContext.Session.SetString("UserEmail", user.Email ?? "Unknown");
            HttpContext.Session.SetString("UserName", user.LastName ?? "Unknown");

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.Combined.AllManagers)]
        public async Task<IActionResult> Profile(string email)
        {
            //var user = await _userManager.FindByEmailAsync(email);
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null) return null;

            var profile = new UserProfileViewModel
            {
                //UserId = user.
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                City = user.City,
                ZipCode = user.ZipCode,
                PhoneNumber = user.PhoneNumber,
                Status = user.Status,
                HireDate = user.HireDate,
                YearsOfExperience = user.YearsOfExperience,
                LastEmployer = user.LastEmployer,
                UserType = user.UserType,

                // Assign role-specific properties
                WorkStationNumber = user.WorkStationNumber,
                PhoneExtension = user.PhoneExtension,
                SpokenLanguage = user.SpokenLanguage,
                BusinessKnowledge = user.BusinessKnowledge,

                OfficeNumber = user.OfficeNumber,
                YearsInManagement = user.YearsInManagement,
                Specialization = user.Specialization,
                ToolBoxNumber = user.ToolBoxNumber,
                AbleToShift = user.AbleToShift,
                WarehouseName = user.WarehouseName,
                WarehouseLocation = user.WarehouseLocation,
                AbleToShiftWareHouse = user.AbleToShiftWareHouse,
                TeamSize = user.TeamSize,
                ImageURL = user.ImageURL,
            };
            //if (!User.IsInRole("Mechanic"))
            //{
            //    return Unauthorized("Access Denied: Your role isn't permitted.");
            //}

            return View(profile);
        }
    }
}
