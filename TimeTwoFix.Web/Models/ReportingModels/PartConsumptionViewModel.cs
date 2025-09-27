namespace TimeTwoFix.Web.Models.ReportingModels
{
    public class PartConsumptionViewModel
    {
        public int SparePartId { get; set; }
        public string PartCode { get; set; } = string.Empty;
        public string PartName { get; set; } = string.Empty;
        public int QuantityUsed { get; set; }
        public decimal TotalValue { get; set; }
    }
}
