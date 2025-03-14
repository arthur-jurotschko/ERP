using AutoMapper;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Data;
using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using GHOST.TalentosCortes.Infra.CrossCutting.AspNetFilters.Filters;
using GHOST.TalentosCortes.Services.API.Configuration;
using GHOST.TalentosCortes.Services.API.Middlewares;

namespace GHOST.TalentosCortes.API
{
    public class StartupMock
    {
        public StartupMock(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Contexto do EF para o Identity
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configurações de Autenticação, Autorização e JWT.
            services.AddMvcSecurity(Configuration);

            // Options para configurações customizadas
            services.AddOptions();

            // MVC com restrição de XML e adição de filtro de ações.
            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
            });

            // Versionamento do WebApi
            services.AddApiVersioning("api/v{version}");

            // AutoMapper
            // Necessário add os assemblies para TestServer
            var assembly = typeof(Program).GetTypeInfo().Assembly;
            services.AddAutoMapper(assembly);

            // MediatR
            services.AddMediatR(typeof(Startup));

            // Registrar todos os DI
            services.AddDIConfiguration();
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IHttpContextAccessor accessor)
        {

            if (env.EnvironmentName == "Testing")
            {
                app.UseSwaggerAuthorized();
            }

            #region Logging

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var elmahSts = new ElmahIoSettings
            {
                OnMessage = message =>
                {
                    message.Version = "v1.0";
                    message.Application = "SystemJurotschko";
                    message.User = accessor.HttpContext.User.Identity.Name;
                },
            };

            loggerFactory.AddElmahIo("e1ce5cbd905b42538c649f6e1d66351e", new Guid("adee8feb-4afb-4d2c-859d-30f729d47793"));
#pragma warning disable CS0618 // O tipo ou membro é obsoleto
            app.UseElmahIo("e1ce5cbd905b42538c649f6e1d66351e", new Guid("adee8feb-4afb-4d2c-859d-30f729d47793"), elmahSts);
#pragma warning restore CS0618 // O tipo ou membro é obsoleto


            #endregion

            #region Configurações MVC

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

            #endregion
        }
    }
}


