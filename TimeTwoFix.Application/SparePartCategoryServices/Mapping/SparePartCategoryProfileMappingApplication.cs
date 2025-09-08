using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTwoFix.Application.SparePartCategoryServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;

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
        }
    }
}
