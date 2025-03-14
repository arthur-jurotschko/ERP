using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace GHOST.TalentosCortes.Services.API.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SystemJurotschko API",
                    Description = "API do site SystemJurotschko",
                    TermsOfService = "Nenhum",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Desenvolvedor X", Email = "email@eventos.io", Url = "http://eventos.io" },
                    License = new Swashbuckle.AspNetCore.Swagger.License { Name = "MIT", Url = "http://eventos.io/licensa" }
                });

                s.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });

            services.ConfigureSwaggerGen(opt =>
            {
                opt.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });
        }

    }
}

