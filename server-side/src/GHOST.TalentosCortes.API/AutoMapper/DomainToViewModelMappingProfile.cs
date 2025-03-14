using AutoMapper;
using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes;
using GHOST.TalentosCortes.Domain.Entities.MasterUsers;
using GHOST.TalentosCortes.Services.API.ViewModels;

namespace GHOST.TalentosCortes.Services.API.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            
            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<MasterUser, MasterUserViewModel>();
            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<Genero, GeneroViewModel>();
            CreateMap<Dashboard, DashboardViewModel>();

        }
    }
}
