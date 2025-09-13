using Microsoft.AspNetCore.Mvc.ModelBinding;
using TimeTwoFix.Web.Models.SparePartCategoryModel;

namespace TimeTwoFix.Web.Models.SparePartModels
{
    public class ReadSparePartViewModel
    {
        public int Id { get; set; }
        public int SparePartCategoryId { get; set; }
        [BindNever]
        public ReadSparePartCategoryViewModel CategoryViewModel { get; set; }
        public string PartCode { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int QuantityInStock { get; set; }
    }
}