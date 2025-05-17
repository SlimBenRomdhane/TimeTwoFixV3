using System.ComponentModel.DataAnnotations.Schema;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.UserManagement;

namespace TimeTwoFix.Core.Entities.SkillManagement
{
    public class MechanicSkill : BaseEntity
    {
        [ForeignKey("Mechanic")]
        public int MechanicId { get; set; }

        public Mechanic Mechanic { get; set; }

        [ForeignKey("Skill")]
        public int SkillId { get; set; }

        public Skill Skill { get; set; }
        public int ProficiencyLevel { get; set; } // 1 to 5 scale
        public int YearsOfExperience { get; set; }
    }
}