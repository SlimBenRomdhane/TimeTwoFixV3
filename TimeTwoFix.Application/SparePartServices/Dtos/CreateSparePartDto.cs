namespace TimeTwoFix.Application.SparePartServices.Dtos
{
    public class CreateSparePartDto
    {
        public int SparePartCategoryId { get; set; }
        public string PartCode { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int QuantityInStock { get; set; }
    }
}