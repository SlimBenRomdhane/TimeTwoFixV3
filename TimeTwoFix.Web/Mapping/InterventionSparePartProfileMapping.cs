using AutoMapper;
using TimeTwoFix.Application.InterventionSparePartServices.Dtos;
using TimeTwoFix.Web.Models.InterventionSparePartModel;

namespace TimeTwoFix.Web.Mapping
{
    public class InterventionSparePartProfileMapping : Profile
    {
        public InterventionSparePartProfileMapping()
        {
            // CreateMap<Source, Destination>();
            CreateMap<ReadInterventionSparePartViewModel, ReadInterventionSparePartDto>()
                 .ForMember(dest => dest.ReadSparePart, opt => opt.MapFrom(src => src.ReadSparePart))

                .ReverseMap();
            CreateMap<CreateInterventionSparePartViewModel, CreateInterventionSparePartDto>().ReverseMap();
            CreateMap<UpdateInterventionSparePartViewModel, UpdateInterventionSparePartDto>().ReverseMap();
            CreateMap<DeleteInterventionSparePartViewModel, DeleteInterventionSparePartDto>().ReverseMap();
        }
    }
}