using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.SparePartCategoryServices.Dtos
{
    public class SparePartCategoryWithUsageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UsageCount { get; set; }
    }
}
