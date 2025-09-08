using AutoMapper;
using TimeTwoFix.Application.InterventionService.Dtos;
using TimeTwoFix.Web.Models.InterventionModels;

namespace TimeTwoFix.Web.Mapping
{
    public class InterventionProfileMapping : Profile
    {
        public InterventionProfileMapping()
        {
            CreateMap<CreateInterventionViewModel, CreateInterventionDto>().ReverseMap();
            CreateMap<ReadInterventionViewModel, ReadInterventionDto>()
                .ForMember(dest => dest.ProvidedServiceDto, opt => opt.MapFrom(src => src.ProvidedService))
                .ForMember(dest => dest.LiftingBridgeDto, opt => opt.MapFrom(src => src.LiftingBridge))
                .ForMember(dest => dest.UserDto, opt => opt.MapFrom(src => src.UserViewModel))
                .ReverseMap();
            CreateMap<UpdateInterventionViewModel, UpdateInterventionDto>().ReverseMap();
            CreateMap<DeleteInterventionViewModel, DeleteInterventionDto>().ReverseMap();
        }
    }
}