using TimeTwoFix.Core.Entities.SkillManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Core.Interfaces.Repositories.SkillsManagement
{
    public interface IMechanicSkillRepository : IBaseRepository<MechanicSkill>
    {
        Task<IEnumerable<MechanicSkill>> GetMechanicSkillsByMechanicIdAsync(int mechanicId);

        Task<IEnumerable<MechanicSkill>> GetMechanicSkillsBySkillIdAsync(int skillId);

        Task<IEnumerable<MechanicSkill>> GetMechanicSkillsByLevelAsync(int level);
    }
}