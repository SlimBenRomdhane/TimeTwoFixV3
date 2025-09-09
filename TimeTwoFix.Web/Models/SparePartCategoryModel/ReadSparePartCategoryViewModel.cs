using TimeTwoFix.Application.Base;

namespace TimeTwoFix.Web.Models.SparePartCategoryModel
{
    public class ReadSparePartCategoryViewModel : AuditClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Notes { get; set; }
    }
}
