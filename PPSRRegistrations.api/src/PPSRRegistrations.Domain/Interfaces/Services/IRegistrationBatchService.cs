using PPSRRegistrations.Domain.Models;

namespace PPSRRegistrations.Domain.Interfaces.Services
{
    public interface IRegistrationBatchService : IDisposable
    {
        Task<RegistrationBatch> Insert(string fileName);
    }
}
