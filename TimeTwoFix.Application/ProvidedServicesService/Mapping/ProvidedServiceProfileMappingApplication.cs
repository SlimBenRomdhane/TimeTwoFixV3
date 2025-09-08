using AutoMapper;
using TimeTwoFix.Application.ProvidedServicesService.Dtos;
using TimeTwoFix.Core.Entities.ServiceManagement;

namespace TimeTwoFix.Application.ProvidedServicesService.Mapping
{
    public class ProvidedServiceProfileMappingApplication : Profile
    {
        public ProvidedServiceProfileMappingApplication()
        {
            CreateMap<ProvidedService, ReadProvidedServiceDto>()
                .ForMember(dest => dest.CategoryDto, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();
            CreateMap<ProvidedService, CreateProvidedServiceDto>().ReverseMap();
            CreateMap<ProvidedService, UpdateProvidedServiceDto>().ReverseMap();
            CreateMap<ProvidedService, DeleteProvidedServiceDto>()
                .ForMember(dest => dest.DeleteCategoryDto, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();
        }
    }
}