using AutoMapper;
using TimeTwoFix.Application.SparePartServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;

namespace TimeTwoFix.Application.SparePartServices.Mapping
{
    public class SparePartProfileMappingApplication : Profile
    {
        public SparePartProfileMappingApplication()
        {
            CreateMap<SparePart, CreateSparePartDto>().ReverseMap();
            CreateMap<SparePart, ReadSparePartDto>().ReverseMap();
            CreateMap<SparePart, UpdateSparePartDto>().ReverseMap();
            CreateMap<SparePart, DeleteSparePartDto>().ReverseMap();
        }
    }
}