using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using PPSRRegistrations.Application.Interfaces;
using PPSRRegistrations.Application.Specifications;
using PPSRRegistrations.Application.ViewModels;
using PPSRRegistrations.Domain.Exceptions;
using PPSRRegistrations.Domain.Interfaces.Services;
using PPSRRegistrations.Domain.Interfaces.UoW;
using PPSRRegistrations.Domain.Models;
using PPSRRegistrations.Domain.Specification;
using System.Globalization;

namespace PPSRRegistrations.Application
{
    public class RegistrationAppService : ApplicationService, IRegistrationAppService
    {
        private readonly IRegistrationService _service;
        private readonly IRegistrationBatchService _batchService;
        private readonly IMapper _mapper;

        public RegistrationAppService(IUnitOfWork unitOfWork, 
                                IMapper mapper,
                                IRegistrationService service,
                                IRegistrationBatchService batchService) : base(unitOfWork)
        {
            _service = service;
            _mapper = mapper;
            _batchService = batchService;
        }

        public async Task<SummaryViewModel> ProcessCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new BusinessException("CSV file is required.", 400); // Bad request

            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<CsvRecordViewModel>().ToList();

            if (!records.Any())
                throw new BusinessException("CSV must have headers and at least one data row.");

            var batch = await _batchService.Insert(file.FileName);
            await Commit();

            var validEntities = new List<Registration>();
            var invalidRecords = new List<CsvRecordViewModel>();
            int submitted = 0;

            foreach (var record in records)
            {
                submitted++;

                if (record.IsValid<CsvRecordViewModel, RegistrationSpecification>(false))
                {
                    var entity = _mapper.Map<Registration>(record);
                    entity.RegistrationBatchId = batch.Id;

                    validEntities.Add(entity);
                }
                else
                {
                    invalidRecords.Add(record);
                }
            }

            int added = 0, updated = 0;
            if (validEntities.Any())
            {
                (added, updated) = await _service.Upsert(validEntities);

                await Commit();
            }

            return new SummaryViewModel
            {
                Submitted = submitted,
                Invalid = invalidRecords.Count,
                Processed = validEntities.Count,
                Updated = updated,
                Added = added
            };
        }

        public void Dispose()
        {
            _service.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
