using AutoMapper;
using TimeTwoFix.Application.SparePartCategoryServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces.Repositories.SparePartManagement;

namespace TimeTwoFix.Application.SparePartCategoryServices.Mapping
{
    public class SparePartCategoryProfileMappingApplication : Profile
    {
        public SparePartCategoryProfileMappingApplication()
        {

            CreateMap<SparePartCategory, ReadSparePartCategoryDto>().ReverseMap();
            CreateMap<SparePartCategory, CreateSparePartCategoryDto>().ReverseMap();
            CreateMap<SparePartCategory, UpdateSparePartCategoryDto>().ReverseMap();
            CreateMap<SparePartCategory, DeleteSparePartCategoryDto>().ReverseMap();
            CreateMap<SparePartCategoryWithUsage, SparePartCategoryWithUsageDto>().ReverseMap();
        }
    }
}
