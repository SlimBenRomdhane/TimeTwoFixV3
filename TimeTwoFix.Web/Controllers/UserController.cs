using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Application.UserServices.Dtos.Users;
using TimeTwoFix.Core.Entities.UserManagement;
using TimeTwoFix.Core.Interfaces;
using TimeTwoFix.Web.Models.RoleModels;
using TimeTwoFix.Web.Models.UserModels;

namespace TimeTwoFix.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var check = await _unitOfWork.ApplicationUsers.RoleExistsAsync(createRoleViewModel.RoleName);
                if (!check)
                {
                    var roleName = new ApplicationRole
                    {
                        Name = createRoleViewModel.RoleName,
                        Description = createRoleViewModel.Description,
                        IsActive = createRoleViewModel.IsActive
                    };
                    var result = await _unitOfWork.ApplicationUsers.CreateRoleAsync(roleName);
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

        public async Task<IActionResult> Index()
        {
            var users = (await _unitOfWork.ApplicationUsers.GetAllUsers()).ToList();
            var roles = (await _unitOfWork.ApplicationUsers.GetAllRoles()).ToList();
            var userViewModel = _mapper.Map<IEnumerable<ReadUserViewModel>>(users).ToList();
            var roleViewModel = _mapper.Map<IEnumerable<ReadRoleViewModel>>(roles).ToList();

            //var mechanics = users.OfType<Mechanic>().ToList();
            //var assistants = users.OfType<FrontDeskAssistant>().ToList();
            //var managers = users.OfType<GeneralManager>().ToList();
            //var warehouseManagers = users.OfType<WareHouseManager>().ToList();
            //var workshopManagers = users.OfType<WorkshopManager>().ToList();
            //var userView = _mapper.Map<IEnumerable<ReadUserViewModel>>(users);

            var viewModel = new Tuple<List<ReadUserViewModel>, List<ReadRoleViewModel>>(userViewModel, roleViewModel);
            return View(viewModel);
        }

        public IActionResult CreateMechanic()
        {
            return View();
        }

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
                    var mechanicDto = _mapper.Map<ReadUserDto>(createMechanicViewModel);
                    var mechanic = _mapper.Map<Mechanic>(mechanicDto);
                    mechanic.UserName = createMechanicViewModel.Email;
                    mechanic.Status = "Active";
                    var result = await _unitOfWork.ApplicationUsers.CreateUserAsync(mechanic, createMechanicViewModel.Password);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create user");
                    }
                    var res = await _unitOfWork.ApplicationUsers.AddUserToRoleAsync(mechanic, "Mechanic");
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

        public IActionResult CreateAssistant()
        {
            return View();
        }

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
                    var assistantDto = _mapper.Map<ReadUserDto>(createFrontDeskAssistantViewModel);
                    var assistant = _mapper.Map<FrontDeskAssistant>(assistantDto);
                    assistant.UserName = createFrontDeskAssistantViewModel.Email;
                    assistant.Status = "Active";
                    var result = await _unitOfWork.ApplicationUsers.CreateUserAsync(assistant, createFrontDeskAssistantViewModel.Password);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create user");
                    }
                    var res = await _unitOfWork.ApplicationUsers.AddUserToRoleAsync(assistant, "FrontDeskAssistant");
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

        public IActionResult CreateWareHouseManager()
        {
            return View();
        }

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
                    var warehouseManagerDto = _mapper.Map<ReadUserDto>(createWareHouseManagerViewModel);
                    var warehouseManager = _mapper.Map<WareHouseManager>(warehouseManagerDto);
                    warehouseManager.UserName = createWareHouseManagerViewModel.Email;
                    warehouseManager.Status = "Active";
                    var result = await _unitOfWork.ApplicationUsers.CreateUserAsync(warehouseManager, createWareHouseManagerViewModel.Password);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create user");
                    }
                    var res = await _unitOfWork.ApplicationUsers.AddUserToRoleAsync(warehouseManager, "WareHouseManager");
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

        public IActionResult CreateWorkshopManager()
        {
            return View();
        }

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
                    var workshopManagerDto = _mapper.Map<ReadUserDto>(createWorkshopManagerViewModel);
                    var workshopManager = _mapper.Map<WorkshopManager>(workshopManagerDto);
                    workshopManager.UserName = createWorkshopManagerViewModel.Email;
                    workshopManager.Status = "Active";
                    var result = await _unitOfWork.ApplicationUsers.CreateUserAsync(workshopManager, createWorkshopManagerViewModel.Password);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create user");
                    }
                    var res = await _unitOfWork.ApplicationUsers.AddUserToRoleAsync(workshopManager, "WorkshopManager");
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

        public IActionResult CreateGeneralManager()
        {
            return View();
        }

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
                    var generalManagerDto = _mapper.Map<ReadUserDto>(createGeneralManagerViewModel);
                    var generalManager = _mapper.Map<GeneralManager>(generalManagerDto);
                    generalManager.UserName = createGeneralManagerViewModel.Email;
                    generalManager.Status = "Active";
                    var result = await _unitOfWork.ApplicationUsers.CreateUserAsync(generalManager, createGeneralManagerViewModel.Password);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create user");
                    }
                    var res = await _unitOfWork.ApplicationUsers.AddUserToRoleAsync(generalManager, "GeneralManager");
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
    }
}