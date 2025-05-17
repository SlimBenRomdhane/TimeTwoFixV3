using Microsoft.EntityFrameworkCore.Storage;
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

namespace TimeTwoFix.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Clients { get; }
        IVehicleRepository Vehicles { get; }
        IWorkOrderRepository WorkOrders { get; }
        IInterventionRepository Interventions { get; }
        IInterventionSparePartRepository InterventionSpareParts { get; }
        ICategoryRepository Categories { get; }
        IServiceRepository Services { get; }
        ILiftingBridgeRepository LiftingBridges { get; }
        ISkillRepository Skills { get; }
        IMechanicSkillRepository MechanicSkills { get; }
        IAppointmentRepository Appointments { get; }
        ISparePartRepository SpareParts { get; }
        IApplicationUserRepository ApplicationUsers { get; }

        //Till Now No Need For These Repositories Bacause ApplicationUserRepository Is Enough
        //IFrontDeskAssistantRepository FrontDeskAssistants { get; }
        //IMechanicRepository Mechanics { get; }
        //IWareHouseManagerRepository WareHouseManagers { get; }
        //IWorkshopManagerRepository WorkshopManagers { get; }
        //IGeneralManagerRepository GeneralManagers { get; }

        Task<int> SaveChangesAsync();

        IBaseRepository<T> GetRepository<T>() where T : class;

        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}