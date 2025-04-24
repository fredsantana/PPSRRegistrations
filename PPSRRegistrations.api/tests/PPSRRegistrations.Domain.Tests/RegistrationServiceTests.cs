using Moq;
using PPSRRegistrations.Domain.Interfaces.Repositories;
using PPSRRegistrations.Domain.Models;
using PPSRRegistrations.Domain.Services;
using System.Linq.Expressions;

namespace PPSRRegistrations.Domain.Tests
{
    public class RegistrationServiceTests
    {
        [Fact]
        public async Task Upsert_ShouldAddCorrectly()
        {
            var repositoryMock = new Mock<IRegistrationRepository>();
            var newEntity = new Registration
            {
                GrantorFirstName = "Bryson",
                GrantorMiddleNames = "James",
                GrantorLastName = "Bernal",
                VIN = "2GCEC19Z8L1159877",
                RegistrationStartDate = new DateOnly(2025, 2, 23),
                RegistrationDuration = "7",
                SPGACN = "001000004",
                SPGOrganizationName = "Company A",
                RegistrationBatchId = Guid.NewGuid(),
            };

            repositoryMock.Setup(r => r.Find(It.IsAny<Expression<Func<Registration, bool>>>()))
                          .ReturnsAsync(new List<Registration>());

            var service = new RegistrationService(repositoryMock.Object);

            var result = await service.Upsert(new List<Registration> { newEntity });

            Assert.Equal((1, 0), result);
        }

        [Fact]
        public async Task Upsert_ShouldUpdateExistingRecord()
        {
            var existing = new Registration
            {
                GrantorFirstName = "Bryson",
                GrantorMiddleNames = "James",
                GrantorLastName = "Bernal",
                VIN = "2GCEC19Z8L1159877",
                RegistrationStartDate = new DateOnly(2025, 2, 23),
                RegistrationDuration = "7",
                SPGACN = "001000004",
                SPGOrganizationName = "Company A",
                RegistrationBatchId = Guid.NewGuid(),
            };

            var newEntity = new Registration
            {
                GrantorFirstName = "Bryson",
                GrantorMiddleNames = "James",
                GrantorLastName = "Bernal",
                VIN = "2GCEC19Z8L1159877",
                RegistrationStartDate = new DateOnly(2025, 2, 23),
                RegistrationDuration = "7",
                SPGACN = "001000004",
                SPGOrganizationName = "Updated Name",
                RegistrationBatchId = Guid.NewGuid(),
            };

            var mockRepo = new Mock<IRegistrationRepository>();
            mockRepo.Setup(r => r.Find(It.IsAny< Expression<Func<Registration, bool>>>()))
                    .ReturnsAsync(new List<Registration> { existing });
            mockRepo.Setup(r => r.Add(It.IsAny<Registration>())).Returns(Task.CompletedTask);

            var service = new RegistrationService(mockRepo.Object);
            var (added, updated) = await service.Upsert(new List<Registration> { newEntity });

            Assert.Equal(0, added);
            Assert.Equal(1, updated);
            Assert.Equal("Updated Name", existing.SPGOrganizationName);
        }

    }
}
