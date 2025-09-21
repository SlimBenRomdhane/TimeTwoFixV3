using AutoMapper;
using TimeTwoFix.Application.VehicleServices.Dtos;
using TimeTwoFix.Web.Models.VehicleModels;

namespace TimeTwoFix.Web.Mapping

{
    public class VehicleProfileMapping : Profile
    {
        public VehicleProfileMapping()
        {
            CreateMap<ReadVehicleViewModel, ReadVehicleDto>()
                .ForMember(dest => dest.ReadClientDto, opt => opt.MapFrom(src => src.ReadClientViewModel))
                .ForMember(dest => dest.WorkOrders, opt => opt.MapFrom(src => src.WorkOrders)) // ✅ Add this
                .ReverseMap();
            CreateMap<CreateVehicleViewModel, CreateVehicleDto>().ReverseMap();
            CreateMap<UpdateVehicleViewModel, UpdateVehicleDto>().ReverseMap();
            CreateMap<DeleteVehicleViewModel, DeleteVehicleDto>().ReverseMap();
        }
    }
}