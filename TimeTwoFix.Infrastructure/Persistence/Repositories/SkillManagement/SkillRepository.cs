using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.SkillManagement;
using TimeTwoFix.Core.Interfaces.Repositories.SkillsManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.SkillManagement
{
    public class SkillRepository : BaseRepository<Skill>, ISkillRepository
    {
        public SkillRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Skill>> GetSkillsByNameAsync(string name)
        {
            return await _context.Skills
                .Where(s => s.Name.Contains(name))
                .ToListAsync();
        }
    }
}