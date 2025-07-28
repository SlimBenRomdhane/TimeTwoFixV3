using Microsoft.Extensions.DependencyInjection;
using TimeTwoFix.Application.AppointmentServices.Interfaces;
using TimeTwoFix.Application.AppointmentServices.Services;
using TimeTwoFix.Application.ClientServices.Interfaces;
using TimeTwoFix.Application.ClientServices.Services;
using TimeTwoFix.Application.LiftingBridgeServices.Interfaces;
using TimeTwoFix.Application.LiftingBridgeServices.Services;
using TimeTwoFix.Application.UserServices.Interfaces;
using TimeTwoFix.Application.UserServices.Services;
using TimeTwoFix.Application.VehicleServices.Interfaces;
using TimeTwoFix.Application.VehicleServices.Services;

namespace TimeTwoFix.Application.Extension
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IClientServices, ClientService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<ILiftingBridgeServices, LifitingBridgeServices>();


            return services;
        }
    }
}