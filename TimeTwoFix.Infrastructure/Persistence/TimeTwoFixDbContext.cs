using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.AppointmentManagement;
using TimeTwoFix.Core.Entities.BridgeManagement;
using TimeTwoFix.Core.Entities.ClientManagement;
using TimeTwoFix.Core.Entities.ServiceManagement;
using TimeTwoFix.Core.Entities.SkillManagement;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Entities.UserManagement;
using TimeTwoFix.Core.Entities.VehicleManagement;
using TimeTwoFix.Core.Entities.WorkOrderManagement;
using TimeTwoFix.Infrastructure.Persistence.DbInit;

namespace TimeTwoFix.Infrastructure.Persistence
{
    public class TimeTwoFixDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public TimeTwoFixDbContext(DbContextOptions<TimeTwoFixDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<InterventionSparePart> InterventionSpareParts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<LiftingBridge> LiftingBridges { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<MechanicSkill> MechanicSkills { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<FrontDeskAssistant> FrontDeskAssistants { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<WareHouseManager> WareHouseManagers { get; set; }
        public DbSet<WorkshopManager> WorkshopManagers { get; set; }
        public DbSet<GeneralManager> GeneralManagers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>(x =>
            {
                x.ToTable("Employees");
                x.Property(c => c.UserName).HasMaxLength(50);
                x.Property(c => c.NormalizedUserName).HasMaxLength(50);
                x.Ignore(c => c.PhoneNumberConfirmed);
                x.Ignore(c => c.LockoutEnabled);
                x.Ignore(c => c.LockoutEnd);
                x.Ignore(c => c.TwoFactorEnabled);
                x.Ignore(c => c.SecurityStamp);

                x.Ignore(c => c.AccessFailedCount);
                x.Ignore(c => c.NormalizedEmail);
                x.HasDiscriminator<string>("RoleTypeDiscriminator")
                    .HasValue<FrontDeskAssistant>(nameof(FrontDeskAssistant))
                    .HasValue<Mechanic>(nameof(Mechanic))
                    .HasValue<WareHouseManager>(nameof(WareHouseManager))
                    .HasValue<WorkshopManager>(nameof(WorkshopManager))
                    .HasValue<GeneralManager>(nameof(GeneralManager));
            });
            modelBuilder.Entity<ApplicationRole>(x =>
            {
                x.ToTable("Roles");
                x.Ignore(c => c.ConcurrencyStamp);
            });
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            /*modelBuilder.Entity<FrontDeskAssistant>().ToTable("FrontDeskAssistants");
            modelBuilder.Entity<Mechanic>().ToTable("Mechanics");
            modelBuilder.Entity<WareHouseManager>().ToTable("WareHouseManagers");
            modelBuilder.Entity<WorkshopManager>().ToTable("WorkshopManagers");*/
            modelBuilder.Entity<Client>(x => x.HasIndex(c => c.Email).IsUnique());
            modelBuilder.Entity<Vehicle>(x => x.HasIndex(v => v.Vin).IsUnique());
            modelBuilder.Entity<SparePart>(x => x.HasIndex(sp => sp.PartCode).IsUnique());
            modelBuilder.Entity<InterventionSparePart>(x => x.HasIndex(isp => isp.DeliveryNote).IsUnique());
            modelBuilder.Entity<Client>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.SeedRoles();
        }
    }
}