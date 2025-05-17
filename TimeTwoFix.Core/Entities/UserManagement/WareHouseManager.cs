using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Core.Entities.UserManagement
{
    public class WareHouseManager : ApplicationUser
    {
        [MaxLength(50)]
        public string? WarehouseName { get; set; }

        [MaxLength(50)]
        public string? WarehouseLocation { get; set; }

        public bool AbleToShift { get; set; }
    }
}