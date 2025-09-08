using AutoMapper;
using TimeTwoFix.Application.WorkOrderService.Dtos;
using TimeTwoFix.Core.Entities.WorkOrderManagement;

namespace TimeTwoFix.Application.WorkOrderService.Mapping
{
    public class WorkOrderProfileMappingApplication : Profile
    {
        public WorkOrderProfileMappingApplication()
        {
            CreateMap<WorkOrder, ReadWorkOrderDto>()
                .ForMember(dest => dest.VehicleDto, opt => opt.MapFrom(src => src.Vehicle))
                .ForMember(dest => dest.InterventionDtos, opt => opt.MapFrom(src => src.Interventions))
                .ReverseMap();
            CreateMap<CreateWorkOrderDto, WorkOrder>();
            CreateMap<UpdateWorkOrderDto, WorkOrder>();
            CreateMap<DeleteWorkOrderDto, WorkOrder>()
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.VehicleDto))
                .ReverseMap();
            CreateMap<WorkOrder, UpdateWorkOrderDto>();
        }
    }
}