using Microsoft.AspNetCore.Identity;
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

        public UnitOfWork(TimeTwoFixDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            Clients = new ClientRepository(_context);
            Categories = new CategoryRepository(_context);
            Services = new ServiceRepository(_context);
            LiftingBridges = new LiftingBridgeRepository(_context);
            Skills = new SkillRepository(_context);
            Appointments = new AppointmentRepository(_context);
            InterventionSpareParts = new InterventionSparePartRepository(_context);
            SpareParts = new SparePartRepository(_context);
            Vehicles = new VehicleRepository(_context);
            WorkOrders = new WorkOrderRepository(_context);
            Interventions = new InterventionRepository(_context);
            MechanicSkills = new MechanicSkillRepository(_context);
            ApplicationUsers = new ApplicationUserRepository(userManager, roleManager, signInManager);
        }

        public IClientRepository Clients { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IServiceRepository Services { get; private set; }
        public ILiftingBridgeRepository LiftingBridges { get; private set; }
        public ISkillRepository Skills { get; private set; }
        public IAppointmentRepository Appointments { get; private set; }
        public IInterventionSparePartRepository InterventionSpareParts { get; private set; }
        public ISparePartRepository SpareParts { get; private set; }
        public IVehicleRepository Vehicles { get; private set; }
        public IWorkOrderRepository WorkOrders { get; private set; }
        public IInterventionRepository Interventions { get; private set; }
        public IApplicationUserRepository ApplicationUsers { get; private set; }
        public IMechanicSkillRepository MechanicSkills { get; private set; }

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
            return await _context.SaveChangesAsync();
        }
    }
}