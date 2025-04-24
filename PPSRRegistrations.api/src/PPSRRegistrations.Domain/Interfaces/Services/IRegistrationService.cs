using PPSRRegistrations.Domain.Models;

namespace PPSRRegistrations.Domain.Interfaces.Services
{
    public interface IRegistrationService : IDisposable
    {
        Task<(int, int)> Upsert(List<Registration> entities);
    }
}
