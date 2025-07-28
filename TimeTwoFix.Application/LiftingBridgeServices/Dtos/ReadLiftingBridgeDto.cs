namespace TimeTwoFix.Application.LiftingBridgeServices.Dtos
{
    public class ReadLiftingBridgeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateOnly InstallationDate { get; set; }
        public string Status { get; set; }
        public int LoadCapacity { get; set; } // in tons
        public string Type { get; set; }
        public string Notes { get; set; }
    }
}