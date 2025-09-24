namespace TimeTwoFix.Core.Interfaces.Repositories.ReportingModels
{
    public class PartConsumptionResult
    {
        public int SparePartId { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
        public int QuantityUsed { get; set; }
        public decimal TotalValue { get; set; } // QuantityUsed * UnitPrice
    }

}