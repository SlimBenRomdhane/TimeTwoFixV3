using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.WorkOrderManagement;

namespace TimeTwoFix.Core.Entities.SparePartManagement
{
    public class InterventionSparePart : BaseEntity
    {
        [ForeignKey("Intervention")]
        public int InterventionId { get; set; }

        [ForeignKey("SparePart")]
        public int SparePartId { get; set; }

        [Required]
        [Range(1, 10)]
        public int Quantity { get; set; }

        //Bon de sortie
        [Required]
        [MaxLength(50)]
        public string DeliveryNote { get; set; }

        public Intervention Intervention { get; set; }
        public SparePart SparePart { get; set; }
    }
}