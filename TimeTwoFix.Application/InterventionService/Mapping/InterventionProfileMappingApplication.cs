using AutoMapper;
using TimeTwoFix.Application.InterventionService.Dtos;
using TimeTwoFix.Application.InterventionSparePartServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;
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
                .ForMember(dest => dest.InterventionSparePartsDto, opt => opt.MapFrom(src => src.InterventionSpareParts))
                .ReverseMap();
            CreateMap<InterventionSparePart, ReadInterventionSparePartDto>()
                .ForMember(dest => dest.SparePartName, opt => opt.MapFrom(src => src.SparePart.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.SparePart.UnitPrice))
                .ReverseMap();
        }
    }
}