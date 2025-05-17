using System.ComponentModel.DataAnnotations;
using TimeTwoFix.Core.Entities.WorkOrderManagement;

namespace TimeTwoFix.Core.Entities.UserManagement
{
    public class Mechanic : ApplicationUser
    {
        [MaxLength(50)]
        public string? Specialization { get; set; }

        [Required]
        [MaxLength(50)]
        public string ToolBoxNumber { get; set; }

        //Possibilite de deplacement
        public bool AbleToShift { get; set; }

        public ICollection<Intervention> Interventions { get; set; }
    }
}