using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PPSRRegistrations.Application;
using PPSRRegistrations.Application.Interfaces;
using PPSRRegistrations.Domain.Interfaces.Repositories;
using PPSRRegistrations.Domain.Interfaces.Services;
using PPSRRegistrations.Domain.Interfaces.UoW;
using PPSRRegistrations.Domain.Services;
using PPSRRegistrations.Infra.Data.Context;
using PPSRRegistrations.Infra.Data.Repositories;
using PPSRRegistrations.Infra.Data.UoW;

namespace PPSRRegistrations.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<IRegistrationAppService, RegistrationAppService>();

            // Domain
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IRegistrationBatchService, RegistrationBatchService>();

            // Data
            services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("AppDb"));
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();
            services.AddScoped<IRegistrationBatchRepository, RegistrationBatchRepository>();

        }
    }
}