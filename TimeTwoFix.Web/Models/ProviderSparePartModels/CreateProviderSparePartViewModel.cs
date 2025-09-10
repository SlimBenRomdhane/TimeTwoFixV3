using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTwoFix.Core.Entities.SparePartManagement;

namespace TimeTwoFix.Web.Models.ProviderSparePartModels
{
    public class CreateProviderSparePartViewModel
    {

        public int ProviderId { get; set; }
        public int SparePartId { get; set; }

        [Required]
        public string DeliveryReceipt { get; set; }
        public int QuantityReceived { get; set; }
        public DateTime DateReceived { get; set; }
        [DataType(DataType.Currency)]
        public decimal UnitPriceAtPurchase { get; set; }

        public decimal Discount { get; set; }
        public decimal VAT { get; set; }
        public decimal Margin { get; set; }

    }
}
