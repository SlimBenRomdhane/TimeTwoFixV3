using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.WorkOrderModels
{
    public class CreateWorkOrderViewModel
    {
        [Required]
        public int VehicleId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Mileage must be a non-negative integer.")]
        public int Mileage { get; set; }
        public DateOnly? StartDate { get; set; }
        public TimeOnly? StartTime { get; set; }
        public DateOnly? EndDate { get; set; }
        public TimeOnly? EndTime { get; set; }
        public decimal? TolalLaborCost { get; set; }
        public WorkOrderStatus Status { get; set; } = WorkOrderStatus.Pending;
        public string? Notes { get; set; }
    }
}
