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
            CreateMap<ReadInterventionViewModel, ReadInterventionDto>().ReverseMap();
            CreateMap<UpdateInterventionViewModel, UpdateInterventionDto>().ReverseMap();
            CreateMap<DeleteInterventionViewModel, DeleteInterventionDto>().ReverseMap();
        }
    }

}
