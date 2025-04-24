using PPSRRegistrations.Domain.Interfaces.Repositories;
using PPSRRegistrations.Domain.Models;
using PPSRRegistrations.Infra.Data.Context;

namespace PPSRRegistrations.Infra.Data.Repositories
{
    public class RegistrationBatchRepository : Repository<RegistrationBatch>, IRegistrationBatchRepository
    {
        public RegistrationBatchRepository(AppDbContext context) : base(context)
        {
        }
    }
}
