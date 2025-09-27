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
    }
}
