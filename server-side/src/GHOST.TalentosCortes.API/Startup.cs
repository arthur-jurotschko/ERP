using System;
using AutoMapper;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Data;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Services;
using GHOST.TalentosCortes.Services.API.Configuration;
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
using GHOST.TalentosCortes.Infra.CrossCutting.AspNetFilters.Filters;

namespace GHOST.TalentosCortes.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
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
            services.AddAutoMapper();

            // Configurações do Swagger
            services.AddSwaggerConfig();

            // MediatR
            services.AddMediatR(typeof(Startup));

            // Registrar todos os DI
            services.AddDIConfiguration();

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, AuthMessageSender>();

        }

        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory,
                              IHttpContextAccessor accessor)
        {

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

            app.UseCors(c =>  //habilitar chamadas de outros dominios outras aplicações
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
                // c.WithOrigins("http://"); aceitar metodos apenas dessa origem
                // c.WithMethods("POST"); apenas metedos POST de fora
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

            #endregion

            #region Swagger

            //if (env.IsDevelopment())
            //{
            //    // Se não tiver um token válido no browser não funciona.
            //    // Descomente para ativar a segurança.
            //    app.UseSwaggerAuthorized();
            //}


            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "SystemJurotschko API v1.0");
            });

            #endregion
        }
    }
}
