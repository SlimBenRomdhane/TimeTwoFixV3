using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTwoFix.Core.Common;

namespace TimeTwoFix.Core.Entities.SparePartManagement
{
    [Index(nameof(PartCode), IsUnique = true)]
    public class SparePart : BaseEntity
    {
        [Required]
        [ForeignKey("SparePartCategory")]
        public int SparePartCategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string PartCode { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        private decimal _unitPrice;
        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                _unitPrice = value;
            }
        }
        private int _quantityInStock;

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int QuantityInStock
        {
            get => _quantityInStock;
            set
            {
                _quantityInStock = value;
            }
        }

        // Navigation properties pour le destockage des pièces
        public ICollection<InterventionSparePart> InterventionSpareParts { get; set; }

        // Navigation properties pour l'alimentation des pièces
        public ICollection<ProviderSparePart> ProviderSpareParts { get; set; }

        public SparePartCategory SparePartCategory { get; set; }

        public void IncreaseStock(ProviderSparePart newEntry)
        {
            if (newEntry != null && !newEntry.IsDeleted)
            {
                QuantityInStock += newEntry.QuantityReceived;
            }
        }
        public void DecreaseStock(InterventionSparePart newEntry)
        {
            if (newEntry != null && !newEntry.IsDeleted)
            {
                QuantityInStock -= newEntry.Quantity;
            }
        }
        public void CalculateUnitPrice(ProviderSparePart source)
        {
            if (source == null || source.IsDeleted)
                return;

            decimal basePrice = source.UnitPriceAtPurchase;
            decimal discount = source.Discount;
            decimal vat = source.VAT;
            decimal margin = source.Margin;

            decimal priceAfterDiscount = basePrice - (basePrice * (discount / 100));
            decimal vatAmount = priceAfterDiscount * (vat / 100);
            decimal priceAfterVAT = priceAfterDiscount + vatAmount;
            UnitPrice = priceAfterVAT + (priceAfterVAT * (margin / 100));
        }

    }
}