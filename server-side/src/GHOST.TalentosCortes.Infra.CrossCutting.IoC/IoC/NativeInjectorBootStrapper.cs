using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Commands;
using GHOST.TalentosCortes.Domain.Entities.MasterUsers.Commands;
using GHOST.TalentosCortes.Domain.Core.Notifications;
using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Events;
using GHOST.TalentosCortes.Domain.Entities.MasterUsers.Events;
using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Repository;
using GHOST.TalentosCortes.Infrastructured.Data.Repository;
using GHOST.TalentosCortes.Domain.Entities.MasterUsers.Repository;
using GHOST.TalentosCortes.Domain.Interfaces;
using GHOST.TalentosCortes.Infrastructured.Data.UoW;
using GHOST.TalentosCortes.Infrastructured.Data.Context;
using GHOST.TalentosCortes.Infrastructured.Data.Repository.EventSourcing;
using GHOST.TalentosCortes.Infrastructured.Data.EventSourcing;
using GHOST.TalentosCortes.Domain.Entities.CommandHandlers;
using GHOST.TalentosCortes.Infra.CrossCutting.AspNetFilters.Filters;
using Microsoft.AspNetCore.Http;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Services;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Models;

namespace GHOST.TalentosCortes.Infra.CrossCutting.IoC.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegistrarClienteCommand>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarClienteCommand>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<ExcluirClienteCommand>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<RegistrarMasterUserCommand>, MasterUserCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarEnderecoClienteCommand>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<IncluirEnderecoClienteCommand>, ClienteCommandHandler>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<ClienteAtualizadoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<ClienteExcluidoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<MasterUserRegistradoEvent>, MasterUserEventHandler>();
            services.AddScoped<INotificationHandler<EnderecoEventoAdicionadoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<EnderecoEventoAtualizadoEvent>, ClienteEventHandler>();

            // Infra - Data
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IMasterUserRepository, MasterUserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<TalentosCortesContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();

            // Infra - Identity
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IUser, AspNetUser>();

            // Infra - Filtros
            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<ILogger<GlobalActionLogger>, Logger<GlobalActionLogger>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();
            services.AddScoped<GlobalActionLogger>();
        }
    }
}