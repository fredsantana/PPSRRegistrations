using Microsoft.AspNetCore.Mvc;
using PPSRRegistrations.Application.Interfaces;
using PPSRRegistrations.Application.ViewModels;

namespace PPSRRegistrations.API.Controllers
{
    /// <summary>
    /// API to batch register PPSR motor vehicle registrations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationAppService _appService;

        /// <summary>
        /// RegistrationController Constructor
        /// </summary>
        /// <param name="appService"></param>
        public RegistrationController(IRegistrationAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Import CSV
        /// </summary>
        /// <param name="file">CSV File</param>
        /// <returns>Summary</returns>
        [HttpPost("csv")]
        [RequestSizeLimit(25 * 1024 * 1024)] // 25MB
        [Consumes("multipart/form-data")]
        public async Task<SummaryViewModel> ImportCsv(IFormFile file)
        {
            return await _appService.ProcessCsv(file);
        }
    }
}
