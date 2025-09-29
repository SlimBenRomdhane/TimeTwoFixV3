namespace TimeTwoFix.Web.Models.ReportingModels
{
    public class VehicleInsightViewModel
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int WorkOrderCount { get; set; }
        public double AverageMileage { get; set; }
        public string VIN { get; set; } = string.Empty;
        public decimal TotalSpend { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerPhone { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageRevenue { get; set; }
        public string? LicensePlate { get; set; }  // Alias for VIN for compatibility
        public string? Vin { get; set; }  // Alias for VIN for compatibility
        public int ServiceCount { get; set; }  // Alias for WorkOrderCount for compatibility
    }
}
