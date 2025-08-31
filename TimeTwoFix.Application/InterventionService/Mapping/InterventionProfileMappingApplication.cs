using AutoMapper;
using TimeTwoFix.Application.Base.BaseDtos;
using TimeTwoFix.Application.InterventionService.Dtos;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.WorkOrderManagement;

namespace TimeTwoFix.Application.InterventionService.Mapping
{
    public class InterventionProfileMappingApplication : Profile
    {
        public InterventionProfileMappingApplication()
        {
            CreateMap<Intervention, CreateInterventionDto>().ReverseMap();
            CreateMap<Intervention, DeleteInterventionDto>().ReverseMap();
            CreateMap<Intervention, UpdateInterventionDto>().ReverseMap();
            CreateMap<Intervention, ReadInterventionDto>()
                .ForMember(dest => dest.ProvidedServiceDto, opt => opt.MapFrom(src => src.Service))
                .ForMember(dest => dest.LiftingBridgeDto, opt => opt.MapFrom(src => src.LiftingBridge))
                .ForMember(dest => dest.UserDto, opt => opt.MapFrom(src => src.Mechanic))
                .ReverseMap();

        }
    }
}
