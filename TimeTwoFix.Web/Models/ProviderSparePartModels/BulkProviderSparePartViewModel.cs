namespace TimeTwoFix.Web.Models.ProviderSparePartModels
{
    public class BulkProviderSparePartViewModel
    {
        public int ProviderId { get; set; }
        public List<CreateProviderSparePartViewModel> SpareParts { get; set; }
    }
}
