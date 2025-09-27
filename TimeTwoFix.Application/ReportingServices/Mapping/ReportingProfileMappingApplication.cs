using AutoMapper;
using TimeTwoFix.Application.ReportingServices.Dtos;
using TimeTwoFix.Core.Interfaces.Repositories.ReportingModels;

namespace TimeTwoFix.Application.ReportingServices.Mapping
{
    public class ReportingProfileMappingApplication : Profile
    {
        public ReportingProfileMappingApplication()
        {
            // Work Orders
            CreateMap<WorkOrderSummaryResult, WorkOrderSummaryDto>();

            // Revenue
            CreateMap<RevenueByMonthResult, RevenueByMonthDto>();
            CreateMap<ServiceCategoryResult, ServiceCategoryDto>();

            // Customers & Vehicles
            CreateMap<CustomerInsightResult, CustomerInsightDto>();
            CreateMap<VehicleInsightResult, VehicleInsightDto>();

            // Mechanics
            CreateMap<MechanicPerformanceResult, MechanicPerformanceDto>();
            CreateMap<MechanicPerformanceTrendResult, MechanicPerformanceTrendDto>();

            // Parts & Suppliers
            CreateMap<PartConsumptionResult, PartConsumptionDto>();
            CreateMap<SupplierSpendResult, SupplierSpendDto>();

            // Bridges & Pauses
            //CreateMap<BridgeUtilizationResult, BridgeUtilizationDto>();
            CreateMap<PauseAnalysisResult, PauseAnalysisDto>();

            // Payments
            CreateMap<PaymentAgingResult, PaymentAgingDto>();
        }

    }
}
