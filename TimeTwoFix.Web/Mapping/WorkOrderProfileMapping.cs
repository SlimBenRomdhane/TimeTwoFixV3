using AutoMapper;
using TimeTwoFix.Application.WorkOrderService.Dtos;
using TimeTwoFix.Web.Models.InterventionModels;
using TimeTwoFix.Web.Models.ProvidedServiceModels;
using TimeTwoFix.Web.Models.VehicleModels;
using TimeTwoFix.Web.Models.WorkOrderModels;

namespace TimeTwoFix.Web.Mapping
{
    public class WorkOrderProfileMapping : Profile
    {
        public WorkOrderProfileMapping()
        {
            CreateMap<CreateWorkOrderViewModel, CreateWorkOrderDto>();
            CreateMap<ReadWorkOrderDto, ReadWorkOrderViewModel>()
                .ForMember(dest => dest.VehicleViewModel, opt => opt.MapFrom(src => src.VehicleDto))
                .ForMember(dest => dest.InterventionViewModels, opt => opt.MapFrom(src => src.InterventionDtos))
                .ReverseMap();
            CreateMap<UpdateWorkOrderDto, UpdateWorkOrderViewModel>().ReverseMap();
            CreateMap<DeleteWorkOrderDto, DeleteWorkOrderViewModel>()
                .ForMember(dest => dest.VehicleViewModel, opt => opt.MapFrom(src => src.VehicleDto))
                .ReverseMap();

            //////////////////////////////////////
            ///
            CreateMap<ExportInterventionViewModel, ReadInterventionViewModel>()
    .ForMember(dest => dest.ProvidedService, opt => opt.MapFrom(src => new ReadProvidedServiceViewModel
    {
        Name = src.ServiceName
    }))
    .ForMember(dest => dest.InterventionPrice, opt => opt.MapFrom(src => src.ServiceCost))
    .ForMember(dest => dest.SparePartsUsed, opt => opt.MapFrom(src => src.SpareParts))
    .ReverseMap();


            CreateMap<ExportInterventionViewModel, ReadInterventionViewModel>()
    .ForMember(dest => dest.ProvidedService, opt => opt.MapFrom(src => new ReadProvidedServiceViewModel
    {
        Name = src.ServiceName
    }))
    .ForMember(dest => dest.InterventionPrice, opt => opt.MapFrom(src => src.ServiceCost))
    .ForMember(dest => dest.SparePartsUsed, opt => opt.MapFrom(src => src.SpareParts))
    .ReverseMap();


            CreateMap<ExportSparePartViewModel, InterventionSparePartDisplayViewModel>()
    .ForMember(dest => dest.SparePartName, opt => opt.MapFrom(src => src.Name))
    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
    .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
    .ReverseMap();

            CreateMap<WorkOrderExportViewModel, ReadWorkOrderViewModel>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
    .ForMember(dest => dest.CustomerLastName, opt => opt.MapFrom(src => src.CustomerLastName))
    .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.CustomerPhone))
    .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail))

    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WorkOrderId))
    .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage))
    .ForMember(dest => dest.TolalLaborCost, opt => opt.MapFrom(src => src.LaborCost))
    .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
    .ForMember(dest => dest.VehicleViewModel, opt => opt.MapFrom(src => new ReadVehicleViewModel
    {
        LicensePlate = src.LicensePlate,
        Brand = src.Brand,
        Model = src.Model,
        Vin = src.Vin
    }))
    .ForMember(dest => dest.InterventionViewModels, opt => opt.MapFrom(src => src.Interventions));


            CreateMap<ExportInterventionViewModel, ReadInterventionViewModel>()
    .ForMember(dest => dest.ProvidedService, opt => opt.MapFrom(src => new ReadProvidedServiceViewModel
    {
        Name = src.ServiceName
    }))
    .ForMember(dest => dest.InterventionPrice, opt => opt.MapFrom(src => src.ServiceCost))
    .ForMember(dest => dest.SparePartsUsed, opt => opt.MapFrom(src => src.SpareParts));

            CreateMap<ExportSparePartViewModel, InterventionSparePartDisplayViewModel>()
                .ForMember(dest => dest.SparePartName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));





        }
    }
}