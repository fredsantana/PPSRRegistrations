using Microsoft.EntityFrameworkCore;
using PPSRRegistrations.Infra.Data.Extensions;
using PPSRRegistrations.Infra.Data.Mappings;

namespace PPSRRegistrations.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new RegistrationMap());
            modelBuilder.AddConfiguration(new RegistrationBatchMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
