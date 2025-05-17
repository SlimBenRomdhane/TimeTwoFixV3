using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.BridgeManagement;
using TimeTwoFix.Core.Interfaces.Repositories.BridgeManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.BridgeManagement
{
    public class LiftingBridgeRepository : BaseRepository<LiftingBridge>, ILiftingBridgeRepository
    {
        public LiftingBridgeRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        // Implement the methods from ILiftingBridgeRepository here
        public async Task<IEnumerable<LiftingBridge>> GetLiftingBridgesByLoadCapacityAsync(int loadCapacity)
        {
            return await _context.LiftingBridges
                .Where(l => l.LoadCapacity == loadCapacity)
                .ToListAsync();
        }

        public async Task<IEnumerable<LiftingBridge>> GetLiftingBridgesByStatusAsync(string status)
        {
            return await _context.LiftingBridges
                .Where(l => l.Status == status)
                .ToListAsync();
        }
    }
}