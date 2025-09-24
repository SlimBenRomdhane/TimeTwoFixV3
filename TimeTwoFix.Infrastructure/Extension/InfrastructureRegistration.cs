using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeTwoFix.Core.Entities.UserManagement;
using TimeTwoFix.Core.Interfaces;
using TimeTwoFix.Core.Interfaces.Repositories.ReportingManagement;
using TimeTwoFix.Infrastructure.Persistence;
using TimeTwoFix.Infrastructure.Persistence.Repositories.ReportingManagement;

namespace TimeTwoFix.Infrastructure.Extension
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext Registration
            services.AddDbContext<TimeTwoFixDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //AzureStorage
            //DefaultConnection
            //Identity Registration
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<TimeTwoFixDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();
            //This line is optional because I stumbled into a problem regestering SignIn Manager
            services.AddHttpContextAccessor();

            //Unit of Work Registration
            services.AddScoped<IReportingRepository, ReportingRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Add other infrastructure services here
            //services.AddScoped<IClientRepository, ClientRepository>();
            return services;
        }
    }
}