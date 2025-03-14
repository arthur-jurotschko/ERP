using GHOST.TalentosCortes.Infra.CrossCutting.IoC.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace GHOST.TalentosCortes.Services.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}