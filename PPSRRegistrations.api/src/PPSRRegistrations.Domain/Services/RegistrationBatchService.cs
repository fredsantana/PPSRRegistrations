using PPSRRegistrations.Domain.Exceptions;
using PPSRRegistrations.Domain.Interfaces.Repositories;
using PPSRRegistrations.Domain.Interfaces.Services;
using PPSRRegistrations.Domain.Models;

namespace PPSRRegistrations.Domain.Services
{
    public class RegistrationBatchService : IRegistrationBatchService
    {
        private readonly IRegistrationBatchRepository _repository;

        public RegistrationBatchService(IRegistrationBatchRepository repository)
        {
            _repository = repository;
        }

        public async Task<RegistrationBatch> Insert(string fileName)
        {
            var bach = (await _repository.Find(x => x.FileName == fileName)).FirstOrDefault();
            if (bach != null)
                throw new BusinessException("Batch operation is in progress.", 412);

            bach = new RegistrationBatch { FileName = fileName };
            await _repository.Add(bach);
            return bach;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
