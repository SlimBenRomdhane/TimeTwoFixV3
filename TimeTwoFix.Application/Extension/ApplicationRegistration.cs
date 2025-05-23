using Microsoft.Extensions.DependencyInjection;
using TimeTwoFix.Application.ClientServices.Interfaces;
using TimeTwoFix.Application.UserServices.Interfaces;
using TimeTwoFix.Application.UserServices.Services;

namespace TimeTwoFix.Application.Extension
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IClientServices, TimeTwoFix.Application.ClientServices.Services.ClientService>();
            return services;
        }
    }
}