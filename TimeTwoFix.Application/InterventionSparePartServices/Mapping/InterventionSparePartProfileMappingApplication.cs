using AutoMapper;
using TimeTwoFix.Application.InterventionSparePartServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;

namespace TimeTwoFix.Application.InterventionSparePartServices.Mapping
{
    public class InterventionSparePartProfileMappingApplication : Profile
    {
        public InterventionSparePartProfileMappingApplication()
        {
            CreateMap<InterventionSparePart, ReadInterventionSparePartDto>().ReverseMap();
            CreateMap<InterventionSparePart, UpdateInterventionSparePartDto>().ReverseMap();
            CreateMap<InterventionSparePart, CreateInterventionSparePartDto>().ReverseMap();
            CreateMap<InterventionSparePart, DeleteInterventionSparePartDto>().ReverseMap();
        }
    }
}