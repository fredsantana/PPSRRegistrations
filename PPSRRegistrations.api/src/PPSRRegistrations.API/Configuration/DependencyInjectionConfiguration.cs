using PPSRRegistrations.Infra.CrossCutting.IoC;

namespace PPSRRegistrations.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            BootStrapper.RegisterServices(services);
        }
    }
}
