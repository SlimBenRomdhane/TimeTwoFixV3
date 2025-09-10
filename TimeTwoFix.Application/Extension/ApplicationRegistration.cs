using Microsoft.Extensions.DependencyInjection;
using TimeTwoFix.Application.AppointmentServices.Interfaces;
using TimeTwoFix.Application.AppointmentServices.Services;
using TimeTwoFix.Application.CategoryService.Interfaces;
using TimeTwoFix.Application.ClientServices.Interfaces;
using TimeTwoFix.Application.ClientServices.Services;
using TimeTwoFix.Application.InterventionService.Interfaces;
using TimeTwoFix.Application.InterventionSparePartServices.Interfaces;
using TimeTwoFix.Application.InterventionSparePartServices.Services;
using TimeTwoFix.Application.LiftingBridgeServices.Interfaces;
using TimeTwoFix.Application.PauseRecordService.Interfaces;
using TimeTwoFix.Application.ProvidedServicesService.Interfaces;
using TimeTwoFix.Application.ProvidedServicesService.Services;
using TimeTwoFix.Application.ProviderServices.Interfaces;
using TimeTwoFix.Application.ProviderServices.Services;
using TimeTwoFix.Application.ProviderSparePartServices.Interfaces;
using TimeTwoFix.Application.ProviderSparePartServices.Services;
using TimeTwoFix.Application.SparePartCategoryServices.Interfaces;
using TimeTwoFix.Application.SparePartCategoryServices.Services;
using TimeTwoFix.Application.SparePartServices.Interfaces;
using TimeTwoFix.Application.SparePartServices.Services;
using TimeTwoFix.Application.UserServices.Interfaces;
using TimeTwoFix.Application.UserServices.Services;
using TimeTwoFix.Application.VehicleServices.Interfaces;
using TimeTwoFix.Application.VehicleServices.Services;
using TimeTwoFix.Application.WorkOrderService.Interfaces;

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
            services.AddScoped<ILiftingBridgeServices, LiftingBridgeServices.Services.LiftingBridgeServices>();
            services.AddScoped<ICategoryService, CategoryService.Services.CategoryService>();
            services.AddScoped<IProvidedServiceService, ProvidedServiceService>();
            services.AddScoped<IWorkOrderService, WorkOrderService.Services.WorkOrderService>();
            services.AddScoped<IInterventionService, InterventionService.Services.InterventionService>();
            services.AddScoped<IPauseRecordService, PauseRecordService.Services.PauseRecordService>();
            services.AddScoped<ISparePartService, SparePartService>();
            services.AddScoped<IInterventionSparePartService, InterventionSparePartService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<ISparePartCategoryService, SparePartCategoryService>();
            services.AddScoped<IProviderSparePartService, ProviderSparePartService>();

            return services;
        }
    }
}