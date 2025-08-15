using AutoMapper;
using TimeTwoFix.Application.WorkOrderService.Dtos;
using TimeTwoFix.Web.Models.WorkOrderModels;

namespace TimeTwoFix.Web.Mapping
{
    public class WorkOrderProfileMapping : Profile
    {
        public WorkOrderProfileMapping()
        {
            CreateMap<CreateWorkOrderViewModel, CreateWorkOrderDto>();
            CreateMap<ReadWorkOrderDto, ReadWorkOrderViewModel>()
                .ForMember(dest => dest.VehicleViewModel, opt => opt.MapFrom(src => src.VehicleDto))
                .ReverseMap();
            CreateMap<UpdateWorkOrderDto, UpdateWorkOrderViewModel>().ReverseMap();
            CreateMap<DeleteWorkOrderDto, DeleteWorkOrderViewModel>()
                .ForMember(dest => dest.VehicleViewModel, opt => opt.MapFrom(src => src.VehicleDto))
                .ReverseMap();
        }
    }
}
