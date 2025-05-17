using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.UserManagement;

namespace TimeTwoFix.Infrastructure.Persistence.DbInit
{
    public static class RoleInit
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationRole>().HasData(

                new ApplicationRole
                {
                    Id = 1,
                    Name = "Mechanic",
                    NormalizedName = "MECHANIC",
                    Description = "Mechanic role with access to work orders and interventions.",
                    IsActive = true
                },
                new ApplicationRole
                {
                    Id = 2,
                    Name = "FrontDeskAssistant",
                    NormalizedName = "FRONTDESKASSISTANT",
                    Description = "Front Desk Assistant role with access to client management and appointments.",
                    IsActive = true
                },
                new ApplicationRole
                {
                    Id = 3,
                    Name = "WareHouseManager",
                    NormalizedName = "WAREHOUSEMANAGER",
                    Description = "Warehouse Manager role with access to spare parts and inventory management.",
                    IsActive = true
                },
                new ApplicationRole
                {
                    Id = 4,
                    Name = "WorkshopManager",
                    NormalizedName = "WORKSHOPMANAGER",
                    Description = "Workshop Manager role with access to workshop operations and team management.",
                    IsActive = true
                },
                new ApplicationRole
                {
                    Id = 5,
                    Name = "GeneralManager",
                    NormalizedName = "GENERALMANAGER",
                    Description = "General Manager role with access to all operations and management.",
                    IsActive = true
                }
            );
        }
    }
}