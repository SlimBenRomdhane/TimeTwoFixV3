using AutoMapper;
using TimeTwoFix.Application.AppointmentServices.Dtos;
using TimeTwoFix.Application.VehicleServices.Dtos;
using TimeTwoFix.Core.Entities.AppointmentManagement;
using TimeTwoFix.Core.Entities.VehicleManagement;

namespace TimeTwoFix.Application.AppointmentServices.Mapping
{
    public class AppointmentProfileMappingApplication : Profile
    {
        public AppointmentProfileMappingApplication()
        {
            CreateMap<CreateAppointmentDto, Appointment>();
            CreateMap<Appointment, ReadAppointmentDto>();
            CreateMap<UpdateAppointmentDto, Appointment>().ReverseMap();
            CreateMap<Appointment, ReadAppointmentDto>()
                .ForMember(dest => dest.ReadVehicleDto, opt => opt.MapFrom(src => src.Vehicle))
                .ReverseMap();

        }
    }

}
