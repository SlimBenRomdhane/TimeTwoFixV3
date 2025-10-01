namespace TimeTwoFix.Application.WorkOrderService.Dtos
{
    public class CreateWorkOrderDto
    {
        public int VehicleId { get; set; }
        public int Mileage { get; set; }
        public DateTime StartDate { get; set; }
        //public TimeOnly StartTime { get; set; }
        public DateTime EndDate { get; set; }
        //public TimeOnly EndTime { get; set; }
        public decimal TolalLaborCost { get; set; }
        public string Status { get; set; }
        public bool Paid { get; set; } = false;
        public DateTime? PaymentDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
    }
}