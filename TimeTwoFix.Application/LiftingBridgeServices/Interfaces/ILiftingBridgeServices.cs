using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.LiftingBridgeServices.Dtos;
using TimeTwoFix.Core.Entities.BridgeManagement;

namespace TimeTwoFix.Application.LiftingBridgeServices.Interfaces
{
    public interface ILiftingBridgeServices : IBaseService<LiftingBridge>
    {
        Task<IEnumerable<ReadLiftingBridgeDto>> GetLiftingBridgesByLoadCapacityAsync(int loadCapacity);

        Task<IEnumerable<ReadLiftingBridgeDto>> GetLiftingBridgesByStatusAsync(string status);
    }
}