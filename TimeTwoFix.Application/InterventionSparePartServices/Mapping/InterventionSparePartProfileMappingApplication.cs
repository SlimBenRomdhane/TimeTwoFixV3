using AutoMapper;
using TimeTwoFix.Application.InterventionSparePartServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;

namespace TimeTwoFix.Application.InterventionSparePartServices.Mapping
{
    public class InterventionSparePartProfileMappingApplication : Profile
    {
        public InterventionSparePartProfileMappingApplication()
        {
            CreateMap<InterventionSparePart, ReadInterventionSparePartDto>()
                .ForMember(dest => dest.ReadSparePart, opt => opt.MapFrom(src => src.SparePart))
                .ReverseMap();
            CreateMap<InterventionSparePart, UpdateInterventionSparePartDto>().ReverseMap();
            CreateMap<InterventionSparePart, CreateInterventionSparePartDto>().ReverseMap();
            CreateMap<InterventionSparePart, DeleteInterventionSparePartDto>().ReverseMap();
        }
    }
}