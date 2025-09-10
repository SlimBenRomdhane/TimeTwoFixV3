using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTwoFix.Application.ProviderSparePartServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;

namespace TimeTwoFix.Application.ProviderSparePartServices.Mapping
{
    public class ProviderSparePartProfileMappingApplication : Profile
    {
        public ProviderSparePartProfileMappingApplication()
        {
            CreateMap<ProviderSparePart, CreateProviderSparePartDto>().ReverseMap();
            CreateMap<ProviderSparePart, UpdateProviderSparePartDto>().ReverseMap();
            CreateMap<ProviderSparePart, DeleteProviderSparePartDto>().ReverseMap();
            CreateMap<ProviderSparePart, ReadProviderSparePartDto>().ReverseMap();
        }
    }
}
