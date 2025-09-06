using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.InterventionSparePartServices.Dtos
{
    public class UpdateInterventionSparePartDto
    {
        public int Id { get; set; }
        public int InterventionId { get; set; }
        public int SparePartId { get; set; }
        public int Quantity { get; set; }
        //Bon de sortie       
        public string DeliveryNote { get; set; }
    }
}
