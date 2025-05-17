using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.SkillManagement;
using TimeTwoFix.Core.Interfaces.Repositories.SkillsManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.SkillManagement
{
    public class MechanicSkillRepository : BaseRepository<MechanicSkill>, IMechanicSkillRepository
    {
        public MechanicSkillRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MechanicSkill>> GetMechanicSkillsByMechanicIdAsync(int mechanicId)
        {
            var mechanicSkills = await _context.MechanicSkills
                .Where(ms => ms.MechanicId == mechanicId)
                .ToListAsync();
            return mechanicSkills;
        }

        public async Task<IEnumerable<MechanicSkill>> GetMechanicSkillsBySkillIdAsync(int skillId)
        {
            var mechanicSkills = await _context.MechanicSkills
                .Where(ms => ms.SkillId == skillId)
                .ToListAsync();
            return mechanicSkills;
        }

        public async Task<IEnumerable<MechanicSkill>> GetMechanicSkillsByLevelAsync(int level)
        {
            var mechanicSkills = await _context.MechanicSkills
                .Where(ms => ms.ProficiencyLevel == level)
                .ToListAsync();
            return mechanicSkills;
        }
    }
}