namespace TimeTwoFix.Web.Models.WorkOrderModels
{
    public class UpdateWorkOrderViewModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int Mileage { get; set; }
        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal? TolalLaborCost { get; set; }
        public string Status { get; set; }
        public bool? Paid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Notes { get; set; }
    }
}