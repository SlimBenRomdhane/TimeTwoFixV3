namespace TimeTwoFix.Web.Models.ReportingModels
{
    public class ServiceCategoryViewModel
    {
        public string CategoryName { get; set; } = string.Empty;
        public int WorkOrderCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
