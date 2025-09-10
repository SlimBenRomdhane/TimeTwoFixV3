namespace TimeTwoFix.Web.Models.ProviderSparePartModels
{
    public class ReadProviderSparePartViewModel
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public int SparePartId { get; set; }
        public string DeliveryReceipt { get; set; }
        public int QuantityReceived { get; set; }
        public DateTime DateReceived { get; set; }
        public decimal UnitPriceAtPurchase { get; set; }
        public decimal Discount { get; set; }
        public decimal VAT { get; set; }
        public decimal Margin { get; set; }
    }
}
