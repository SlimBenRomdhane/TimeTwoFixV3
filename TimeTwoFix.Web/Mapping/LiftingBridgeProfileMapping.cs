using AutoMapper;
using TimeTwoFix.Application.LiftingBridgeServices.Dtos;
using TimeTwoFix.Web.Models.LiftingBridgeModels;

namespace TimeTwoFix.Web.Mapping
{
    public class LiftingBridgeProfileMapping : Profile
    {
        public LiftingBridgeProfileMapping()
        {
            CreateMap<CreateLiftingBridgeViewModel, CreateLiftingBridgeDto>().ReverseMap();
            CreateMap<ReadLiftingBridgeViewModel, ReadLiftingBridgeDto>().ReverseMap();
            CreateMap<UpdateLiftingBridgeDto, UpdateLiftingBridgeViewModel>().ReverseMap();
            CreateMap<DeleteLiftingBridgeViewModel, DeleteLiftingBridgeDto>().ReverseMap();
        }
    }
}