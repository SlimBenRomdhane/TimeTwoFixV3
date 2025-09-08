namespace TimeTwoFix.Web.Models.LiftingBridgeModels
{
    public class DeleteLiftingBridgeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateOnly InstallationDate { get; set; }
        public string Status { get; set; }
        public int LoadCapacity { get; set; } // in Kilograms
        public string Type { get; set; }
        public string Notes { get; set; }
    }
}