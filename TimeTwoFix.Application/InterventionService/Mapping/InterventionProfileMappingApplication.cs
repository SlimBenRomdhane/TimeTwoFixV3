using AutoMapper;
using TimeTwoFix.Application.InterventionService.Dtos;
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
            CreateMap<Intervention, ReadInterventionDto>().ReverseMap();
        }
    }
}
