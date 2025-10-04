using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TimeTwoFix.Core.Entities.UserManagement;
using TimeTwoFix.Core.Interfaces;
using TimeTwoFix.Core.Interfaces.Repositories.AppointmentManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;
using TimeTwoFix.Core.Interfaces.Repositories.BridgeManagement;
using TimeTwoFix.Core.Interfaces.Repositories.ClientManagement;
using TimeTwoFix.Core.Interfaces.Repositories.IdentityManagement;
using TimeTwoFix.Core.Interfaces.Repositories.ServiceManagement;
using TimeTwoFix.Core.Interfaces.Repositories.SkillsManagement;
using TimeTwoFix.Core.Interfaces.Repositories.SparePartManagement;
using TimeTwoFix.Core.Interfaces.Repositories.VehicleManagement;
using TimeTwoFix.Core.Interfaces.Repositories.WorkOrderManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.AppointmentManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;
using TimeTwoFix.Infrastructure.Persistence.Repositories.BridgeManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.ClientManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.ServiceManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.SkillManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.SparePartManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.UserManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.VehicleManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.WorkOrderManagement;

namespace TimeTwoFix.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly TimeTwoFixDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UnitOfWork(TimeTwoFixDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        // Lazy-loaded repositories to avoid instantiating all repositories at startup
        private IClientRepository _clients;
        public IClientRepository Clients => _clients ??= new ClientRepository(_context);

        private ICategoryRepository _categories;
        public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);

        private IProvidedServiceRepository _providedServices;
        public IProvidedServiceRepository ProvidedServices => _providedServices ??= new ProvidedServiceRepository(_context);

        private ILiftingBridgeRepository _liftingBridges;
        public ILiftingBridgeRepository LiftingBridges => _liftingBridges ??= new LiftingBridgeRepository(_context);

        private ISkillRepository _skills;
        public ISkillRepository Skills => _skills ??= new SkillRepository(_context);

        private IAppointmentRepository _appointments;
        public IAppointmentRepository Appointments => _appointments ??= new AppointmentRepository(_context);

        private IInterventionSparePartRepository _interventionSpareParts;
        public IInterventionSparePartRepository InterventionSpareParts => _interventionSpareParts ??= new InterventionSparePartRepository(_context);

        private ISparePartRepository _spareParts;
        public ISparePartRepository SpareParts => _spareParts ??= new SparePartRepository(_context);

        private IVehicleRepository _vehicles;
        public IVehicleRepository Vehicles => _vehicles ??= new VehicleRepository(_context);

        private IWorkOrderRepository _workOrders;
        public IWorkOrderRepository WorkOrders => _workOrders ??= new WorkOrderRepository(_context);

        private IInterventionRepository _interventions;
        public IInterventionRepository Interventions => _interventions ??= new InterventionRepository(_context);

        private IApplicationUserRepository _applicationUsers;
        public IApplicationUserRepository ApplicationUsers => _applicationUsers ??= new ApplicationUserRepository(_userManager, _roleManager, _signInManager);

        private IMechanicSkillRepository _mechanicSkills;
        public IMechanicSkillRepository MechanicSkills => _mechanicSkills ??= new MechanicSkillRepository(_context);

        private IProviderRepository _providers;
        public IProviderRepository Providers => _providers ??= new ProviderRepository(_context);

        private ISparePartCategoryRepository _sparePartCategories;
        public ISparePartCategoryRepository SparePartCategories => _sparePartCategories ??= new SparePartCategoryRepository(_context);

        private IProviderSparePartRepository _providerSpareParts;
        public IProviderSparePartRepository ProviderSpareParts => _providerSpareParts ??= new ProviderSparePartRepository(_context);

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _context.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            return new BaseRepository<T>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            var modifiedEntries = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added || e.State == EntityState.Deleted).ToList();
            if (!modifiedEntries.Any())
            {
                return 0;
            }
            return await _context.SaveChangesAsync();
        }
    }
}
