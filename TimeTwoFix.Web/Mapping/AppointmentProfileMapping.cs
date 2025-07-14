using AutoMapper;
using TimeTwoFix.Application.AppointmentServices.Dtos;
using TimeTwoFix.Web.Models.AppointmentModels;

namespace TimeTwoFix.Web.Mapping
{
    public class AppointmentProfileMapping : Profile
    {
        public AppointmentProfileMapping()
        {
            CreateMap<CreateAppointmentViewModel, CreateAppointmentDto>();
            CreateMap<ReadAppointmentViewModel, ReadAppointmentDto>()
               .ForMember(dest => dest.ReadVehicleDto, opt => opt.MapFrom(src => src.ReadVehicleViewModel)).ReverseMap();
            CreateMap<UpdateAppointmentDto, UpdateAppointmentViewModel>().ReverseMap();
            //CreateMap<Models.AppointmentModels.DeleteAppointmentViewModel, Core.Entities.AppointmentManagement.Appointment>();
            CreateMap<DeleteAppointmentViewModel, DeleteAppointmentDto>()
                .ForMember(dest => dest.ReadVehicleDto, opt => opt.MapFrom(src => src.ReadVehicleViewModel))
                .ReverseMap();
        }
    }
}
