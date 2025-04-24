using PPSRRegistrations.Domain.Interfaces.Repositories;
using PPSRRegistrations.Domain.Interfaces.Services;
using PPSRRegistrations.Domain.Models;

namespace PPSRRegistrations.Domain.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _repository;

        public RegistrationService(IRegistrationRepository repository)
        {
            _repository = repository;
        }

        public async Task<(int, int)> Upsert(List<Registration> entities)
        {
            var keyMap = entities.ToDictionary(
                r => $"{r.GrantorFirstName}|{r.GrantorMiddleNames}|{r.GrantorLastName}|{r.VIN}|{r.SPGACN}",
                r => r
            );

            var keys = keyMap.Keys.ToList();

            var existingRegs = await _repository
                .Find(r => keys.Contains(
                    $"{r.GrantorFirstName}|{r.GrantorMiddleNames}|{r.GrantorLastName}|{r.VIN}|{r.SPGACN}"
                ));

            int updated = 0, added = 0;

            foreach (var entity in entities)
            {
                var existing = existingRegs.FirstOrDefault(r =>
                    r.GrantorFirstName == entity.GrantorFirstName &&
                    r.GrantorMiddleNames == entity.GrantorMiddleNames &&
                    r.GrantorLastName == entity.GrantorLastName &&
                    r.VIN == entity.VIN &&
                    r.SPGACN == entity.SPGACN);

                if (existing != null)
                {
                    existing.RegistrationStartDate = entity.RegistrationStartDate;
                    existing.RegistrationDuration = entity.RegistrationDuration;
                    existing.SPGOrganizationName = entity.SPGOrganizationName;
                    existing.RegistrationBatchId = entity.RegistrationBatchId;
                    updated++;
                }
                else
                {
                    await _repository.Add(entity);
                    added++;
                }
            }

            return (added, updated);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
