using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTwoFix.Core.Common;

namespace TimeTwoFix.Core.Entities.SparePartManagement
{
    [Table("StockIncrease")]
    public class ProviderSparePart : BaseEntity
    {
        [ForeignKey("Provider")]
        public int ProviderId { get; set; }
        [ForeignKey("SparePart")]
        public int SparePartId { get; set; }
        [Required]
        public required string DeliveryReceipt { get; set; }
        public int QuantityReceived { get; set; }
        public DateTime DateReceived { get; set; }
        public decimal UnitPriceAtPurchase { get; set; }
        public decimal Discount { get; set; }
        public decimal VAT { get; set; }
        public decimal Margin { get; set; }
        public Provider Provider { get; set; }
        public SparePart SparePart { get; set; }
    }
}
