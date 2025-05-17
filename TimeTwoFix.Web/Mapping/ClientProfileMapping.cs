using AutoMapper;
using TimeTwoFix.Application.ClientServices.Dtos;
using TimeTwoFix.Core.Entities.ClientManagement;
using TimeTwoFix.Web.Models.ClientModels;

namespace TimeTwoFix.Web.Mapping
{
    public class ClientProfileMapping : Profile
    {
        public ClientProfileMapping()
        {
            CreateMap<ReadClientDto, ReadClientViewModel>();
            CreateMap<CreateClientViewModel, CreateClientDto>();
            CreateMap<ReadClientDto, UpdateClientViewModel>().ReverseMap();
            CreateMap<UpdateClientViewModel, UpdateClientDto>().ReverseMap();
            CreateMap<DeleteClientViewModel, DeleteClientDto>().ReverseMap();
            CreateMap<Client, DeleteClientDto>();

            //CreateMap<UpdateClientViewModel, Client>();
        }
    }
}