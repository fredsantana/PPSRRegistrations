using Microsoft.AspNetCore.Http;
using PPSRRegistrations.Application.ViewModels;

namespace PPSRRegistrations.Application.Interfaces
{
    public interface IRegistrationAppService : IDisposable
    {
        Task<SummaryViewModel> ProcessCsv(IFormFile file);
    }
}
