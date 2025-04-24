using PPSRRegistrations.Domain.Interfaces.Repositories;
using PPSRRegistrations.Domain.Models;
using PPSRRegistrations.Infra.Data.Context;

namespace PPSRRegistrations.Infra.Data.Repositories
{
    public class RegistrationRepository : Repository<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
