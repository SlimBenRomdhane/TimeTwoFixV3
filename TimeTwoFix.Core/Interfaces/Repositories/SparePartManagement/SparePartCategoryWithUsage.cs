using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTwoFix.Core.Interfaces.Repositories.SparePartManagement
{
    [NotMapped]
    public class SparePartCategoryWithUsage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UsageCount { get; set; }
    }
}
