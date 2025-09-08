namespace TimeTwoFix.Web.Models.SparePartModels
{
    public class DeleteSparePartViewModel
    {
        public int Id { get; set; }
        public string PartCode { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int QuantityInStock { get; set; }
    }
}