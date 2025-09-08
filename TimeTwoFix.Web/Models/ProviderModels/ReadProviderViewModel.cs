using TimeTwoFix.Application.Base;

namespace TimeTwoFix.Web.Models.ProviderModels
{
    public class ReadProviderViewModel : AuditClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string MobileContactPhone { get; set; }
        public string LandContactPhone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string RIB { get; set; }
        public string FiscalId { get; set; }
    }
}
