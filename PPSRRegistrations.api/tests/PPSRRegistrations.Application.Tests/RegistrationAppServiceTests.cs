using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using PPSRRegistrations.Application.ViewModels;
using PPSRRegistrations.Domain.Exceptions;
using PPSRRegistrations.Domain.Interfaces.Services;
using PPSRRegistrations.Domain.Interfaces.UoW;
using PPSRRegistrations.Domain.Models;
using System.Text;

namespace PPSRRegistrations.Application.Tests
{
    public class RegistrationAppServiceTests
    {
        private readonly Mock<IRegistrationService> _registrationServiceMock;
        private readonly Mock<IRegistrationBatchService> _batchServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly RegistrationAppService _appService;

        public RegistrationAppServiceTests()
        {
            _registrationServiceMock = new Mock<IRegistrationService>();
            _batchServiceMock = new Mock<IRegistrationBatchService>();
            _mapperMock = new Mock<IMapper>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            _appService = new RegistrationAppService(unitOfWorkMock.Object, _mapperMock.Object,
                _registrationServiceMock.Object, _batchServiceMock.Object);
        }

        [Fact]
        public async Task ProcessCsv_ShouldReturnSummary_WhenFileIsValid()
        {
            // Arrange
            var csvContent = "Grantor First Name,Grantor Middle Names,Grantor Last Name,VIN,Registration start date,Registration duration,SPG ACN,SPG Organization Name\n" +
                             "Bryson,James,Bernal,2GCEC19Z8L1159877,2025-02-23,7,001 000 004,Company A";

            var bytes = Encoding.UTF8.GetBytes(csvContent);
            var stream = new MemoryStream(bytes);
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.Length).Returns(stream.Length);
            fileMock.Setup(f => f.FileName).Returns("test.csv");

            var batch = new RegistrationBatch { Id = Guid.NewGuid(), FileName = "test.csv" };
            _batchServiceMock.Setup(s => s.Insert(It.IsAny<string>())).ReturnsAsync(batch);

            var record = new CsvRecordViewModel
            {
                GrantorFirstName = "Bryson",
                GrantorMiddleNames = "James",
                GrantorLastName = "Bernal",
                VIN = "2GCEC19Z8L1159877",
                RegistrationStartDateRaw = "2025-02-23",
                RegistrationDuration = "7",
                SPGACN = "001 000 004",
                SPGOrganizationName = "Company A"
            };

            var entity = new Registration
            {
                GrantorFirstName = "Bryson",
                GrantorMiddleNames = "James",
                GrantorLastName = "Bernal",
                VIN = "2GCEC19Z8L1159877",
                RegistrationStartDate = new DateOnly(2025, 2, 23),
                RegistrationDuration = "7",
                SPGACN = "001000004",
                SPGOrganizationName = "Company A",
                RegistrationBatchId = batch.Id
            };

            _mapperMock.Setup(m => m.Map<Registration>(It.IsAny<CsvRecordViewModel>())).Returns(entity);
            _registrationServiceMock.Setup(s => s.Upsert(It.IsAny<List<Registration>>())).ReturnsAsync((1, 0));

            // Act
            var result = await _appService.ProcessCsv(fileMock.Object);

            // Assert
            Assert.Equal(1, result.Submitted);
            Assert.Equal(0, result.Invalid);
            Assert.Equal(1, result.Processed);
            Assert.Equal(1, result.Added);
            Assert.Equal(0, result.Updated);
        }

        [Fact]
        public async Task ProcessCsv_ShouldThrowException_WhenFileIsNull()
        {
            // Arrange & Act & Assert
            var ex = await Assert.ThrowsAsync<BusinessException>(() => _appService.ProcessCsv(null));
            Assert.Equal("CSV file is required.", ex.Message);
        }

        [Fact]
        public async Task ProcessCsv_ShouldThrowException_WhenFileHasNoRecords()
        {
            // Arrange
            var csvContent = "Grantor First Name,Grantor Middle Names,Grantor Last Name,VIN,Registration start date,Registration duration,SPG ACN,SPG Organization Name\n";
            var bytes = Encoding.UTF8.GetBytes(csvContent);
            var stream = new MemoryStream(bytes);

            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.Length).Returns(stream.Length);
            fileMock.Setup(f => f.FileName).Returns("test.csv");

            // Act & Assert
            var ex = await Assert.ThrowsAsync<BusinessException>(() => _appService.ProcessCsv(fileMock.Object));
            Assert.Equal("CSV must have headers and at least one data row.", ex.Message);
        }
    }
}
