using AutoMapper;
using System.Globalization;
using TimeTwoFix.Application.ReportingServices.Dtos;
using TimeTwoFix.Web.Models.ReportingModels;

namespace TimeTwoFix.Web.Mapping
{
    public class ReportingProfileMapping : Profile
    {
        public ReportingProfileMapping()
        {
            CreateMap<MechanicPerformanceDto, MechanicPerformanceViewModel>();
            CreateMap<MechanicPerformanceTrendDto, MechanicPerformanceTrendViewModel>();
            CreateMap<CustomerInsightDto, CustomerInsightViewModel>();
            CreateMap<PartConsumptionDto, PartConsumptionViewModel>();
            CreateMap<PauseAnalysisDto, PauseAnalysisViewModel>();
            CreateMap<PauseAnalysisTrendDto, PauseAnalysisTrendViewModel>();
            CreateMap<PaymentAgingDto, PaymentAgingViewModel>();
            CreateMap<RevenueByMonthDto, RevenueByMonthViewModel>();
            CreateMap<ServiceCategoryDto, ServiceCategoryViewModel>();
            CreateMap<SupplierSpendDto, SupplierSpendViewModel>();
            CreateMap<VehicleInsightDto, VehicleInsightViewModel>();
            CreateMap<WorkOrderSummaryDto, WorkOrderSummaryViewModel>();
        }
    }
}
