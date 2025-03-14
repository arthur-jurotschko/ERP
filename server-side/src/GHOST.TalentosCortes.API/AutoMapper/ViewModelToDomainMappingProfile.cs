using AutoMapper;
using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Commands;
using GHOST.TalentosCortes.Domain.Entities.MasterUsers.Commands;
using GHOST.TalentosCortes.Services.API.ViewModels;
using System;

namespace GHOST.TalentosCortes.Services.API.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteViewModel, RegistrarClienteCommand>()
                .ConstructUsing(x => new RegistrarClienteCommand(
                    x.NomeCompleto,
                    x.CPF,
                    x.RG,
                    x.TelResidencial,
                    x.TelCelular,
                    x.Email,
                    x.RedeSocial,
                    x.Data,
                    x.CheckEndereco,
                    x.MasterUserId,
                    x.GeneroId,
                    new IncluirEnderecoClienteCommand(
                        x.Endereco.Id,
                        x.Endereco.Logradouro,
                        x.Endereco.Numero,
                        x.Endereco.Complemento,
                        x.Endereco.Bairro,
                        x.Endereco.CEP,
                        x.Endereco.Cidade,
                        x.Endereco.Estado,
                        x.Id)));

            CreateMap<EnderecoViewModel, IncluirEnderecoClienteCommand>()
                .ConstructUsing(x => new IncluirEnderecoClienteCommand(
                    Guid.NewGuid(),
                    x.Logradouro,
                    x.Numero,
                    x.Complemento,
                    x.Bairro,
                    x.CEP,
                    x.Cidade,
                    x.Estado,
                    x.ClienteId));

            CreateMap<EnderecoViewModel, AtualizarEnderecoClienteCommand>()
                .ConstructUsing(x => new AtualizarEnderecoClienteCommand(
                    x.Id,
                    x.Logradouro,
                    x.Numero,
                    x.Complemento,
                    x.Bairro,
                    x.CEP,
                    x.Cidade,
                    x.Estado,
                    x.ClienteId));

            CreateMap<ClienteViewModel, AtualizarClienteCommand>()
                .ConstructUsing(x => new AtualizarClienteCommand(
                    x.Id,                
                    x.NomeCompleto,
                    x.CPF,
                    x.RG,
                    x.TelResidencial,
                    x.TelCelular,
                    x.Email,
                    x.RedeSocial,
                    x.Data,
                    x.CheckEndereco,
                    x.MasterUserId,
                    x.GeneroId
                    ));

            CreateMap<ClienteViewModel, ExcluirClienteCommand>()
                .ConstructUsing(c => new ExcluirClienteCommand(c.Id));

            // MasterUser
            CreateMap<MasterUserViewModel, RegistrarMasterUserCommand>()
                .ConstructUsing(c => new RegistrarMasterUserCommand(c.Id, c.Nome, c.CPF, c.Email));
        }
    }
}