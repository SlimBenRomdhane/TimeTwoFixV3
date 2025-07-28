namespace TimeTwoFix.Web.Models.LiftingBridgeModels
{
    public class ReadLiftingBridgeViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public DateOnly InstallationDate { get; set; }
        public required string Status { get; set; }
        public int LoadCapacity { get; set; } // in Kilograms
        public string Type { get; set; }
        public string Notes { get; set; }
    }
}
