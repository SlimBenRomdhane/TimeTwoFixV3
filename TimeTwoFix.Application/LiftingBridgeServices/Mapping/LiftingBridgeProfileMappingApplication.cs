using AutoMapper;
using TimeTwoFix.Application.LiftingBridgeServices.Dtos;
using TimeTwoFix.Core.Entities.BridgeManagement;

namespace TimeTwoFix.Application.LiftingBridgeServices.Mapping
{
    public class LiftingBridgeProfileMappingApplication : Profile
    {
        public LiftingBridgeProfileMappingApplication()
        {
            CreateMap<LiftingBridge, ReadLiftingBridgeDto>().ReverseMap();
            CreateMap<CreateLiftingBridgeDto, LiftingBridge>();
            CreateMap<UpdateLiftingBridgeDto, LiftingBridge>().ReverseMap();
        }
    }
}
