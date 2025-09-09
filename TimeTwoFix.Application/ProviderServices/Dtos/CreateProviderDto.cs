namespace TimeTwoFix.Application.ProviderServices.Dtos
{
    public class CreateProviderDto
    {
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string MobileContactPhone { get; set; }
        public string LandContactPhone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string RIB { get; set; }
        public string FiscalId { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        string? CreatedBy { get; set; }
    }
}