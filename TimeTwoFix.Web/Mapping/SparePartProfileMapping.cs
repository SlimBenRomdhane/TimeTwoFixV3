using AutoMapper;
using TimeTwoFix.Application.SparePartServices.Dtos;
using TimeTwoFix.Web.Models.SparePartModels;

namespace TimeTwoFix.Web.Mapping
{
    public class SparePartProfileMapping : Profile
    {
        public SparePartProfileMapping()
        {
            CreateMap<CreateSparePartDto, CreateSparePartViewModel>().ReverseMap();
            CreateMap<ReadSparePartDto, ReadSparePartViewModel>()

                .ForMember(dest => dest.CategoryViewModel, opt => opt.MapFrom(src => src.CategoryDto))
                .ReverseMap();
            CreateMap<UpdateSparePartDto, UpdateSparePartViewModel>()
                .ForMember(dest => dest.CategoryViewModel, opt => opt.MapFrom(src => src.CategoryDto))
                .ReverseMap();
            CreateMap<DeleteSparePartDto, DeleteSparePartViewModel>()
                .ForMember(dest => dest.CategoryViewModel, opt => opt.MapFrom(src => src.CategoryDto))
                .ReverseMap();
        }
    }
}