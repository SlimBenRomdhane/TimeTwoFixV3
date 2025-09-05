using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.SparePartServices.Dtos
{
    public class CreateSparePartDto
    {
        public string PartCode { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int QuantityInStock { get; set; }
    }
}
