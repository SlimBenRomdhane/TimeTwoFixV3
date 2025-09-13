using AutoMapper;
using TimeTwoFix.Application.SparePartServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;

namespace TimeTwoFix.Application.SparePartServices.Mapping
{
    public class SparePartProfileMappingApplication : Profile
    {
        public SparePartProfileMappingApplication()
        {
            CreateMap<SparePart, CreateSparePartDto>()
                .ReverseMap();
            CreateMap<SparePart, ReadSparePartDto>()
                .ForMember(dest => dest.CategoryDto, opt => opt.MapFrom(src => src.SparePartCategory))
                .ReverseMap();
            CreateMap<SparePart, UpdateSparePartDto>()
                .ForMember(dest => dest.CategoryDto, opt => opt.MapFrom(src => src.SparePartCategory))
                .ReverseMap();
            CreateMap<SparePart, DeleteSparePartDto>()
                .ForMember(dest => dest.CategoryDto, opt => opt.MapFrom(src => src.SparePartCategory))
                .ReverseMap();
        }
    }
}