using System.ComponentModel.DataAnnotations;
using TimeTwoFix.Core.Common;

namespace TimeTwoFix.Core.Entities.SkillManagement
{
    public class Skill : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}