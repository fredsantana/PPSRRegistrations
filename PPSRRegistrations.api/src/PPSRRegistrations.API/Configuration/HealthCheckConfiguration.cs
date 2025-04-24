using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PPSRRegistrations.API.Configuration
{
    public static class HealthCheckConfiguration
    {
        public static void AddHealthCheckSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" });

            services.AddHealthChecksUI().AddInMemoryStorage();
        }
        
        public static void MapHealthCheckSetup(this IEndpointRouteBuilder endpoints)
        {
            if (endpoints == null) throw new ArgumentNullException(nameof(endpoints));

            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            endpoints.MapHealthChecksUI(options =>
            {
                options.UIPath = "/health-ui";
            });
        }
    }
}
