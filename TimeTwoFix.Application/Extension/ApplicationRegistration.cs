using Microsoft.Extensions.DependencyInjection;
using TimeTwoFix.Application.ClientServices.Interfaces;

namespace TimeTwoFix.Application.Extension
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClientServices, TimeTwoFix.Application.ClientServices.Services.ClientService>();
            return services;
        }
    }
}