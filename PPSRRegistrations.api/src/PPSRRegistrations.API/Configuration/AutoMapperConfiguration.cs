using PPSRRegistrations.Application.AutoMapper;

namespace PPSRRegistrations.API.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutomapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            AutoMapperConfig.RegisterMappings();
        }
    }
}
