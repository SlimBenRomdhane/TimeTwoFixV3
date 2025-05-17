using TimeTwoFix.Core.Entities.BridgeManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Core.Interfaces.Repositories.BridgeManagement
{
    public interface ILiftingBridgeRepository : IBaseRepository<LiftingBridge>
    {
        Task<IEnumerable<LiftingBridge>> GetLiftingBridgesByLoadCapacityAsync(int loadCapacity);

        Task<IEnumerable<LiftingBridge>> GetLiftingBridgesByStatusAsync(string status);
    }
}