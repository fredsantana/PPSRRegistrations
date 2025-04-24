using Moq;
using PPSRRegistrations.Domain.Exceptions;
using PPSRRegistrations.Domain.Interfaces.Repositories;
using PPSRRegistrations.Domain.Models;
using PPSRRegistrations.Domain.Services;
using System.Linq.Expressions;

namespace PPSRRegistrations.Domain.Tests
{
    public class RegistrationBatchServiceTests
    {
        [Fact]
        public async Task Insert_ShouldThrowIfBatchWithSameFilenameExists()
        {
            var repositoryMock = new Mock<IRegistrationBatchRepository>();
            repositoryMock.Setup(r => r.Find(It.IsAny<Expression<Func<RegistrationBatch, bool>>>()))
                          .ReturnsAsync(new List<RegistrationBatch> { new RegistrationBatch { FileName = "file.csv" } });

            var service = new RegistrationBatchService(repositoryMock.Object);

            var exception = await Assert.ThrowsAsync<BusinessException>(() => service.Insert("file.csv"));

            Assert.Equal("Batch operation is in progress.", exception.Message);
        }

        [Fact]
        public async Task Insert_ShouldAddNewBatch()
        {
            var mockRepository = new Mock<IRegistrationBatchRepository>();
            mockRepository.Setup(r => r.Find(It.IsAny<Expression<Func<RegistrationBatch, bool>>>()))
                          .ReturnsAsync(new List<RegistrationBatch>());
            mockRepository.Setup(r => r.Add(It.IsAny<RegistrationBatch>())).Returns(Task.CompletedTask);

            var service = new RegistrationBatchService(mockRepository.Object);
            var result = await service.Insert("test.csv");

            Assert.Equal("test.csv", result.FileName);
        }
    }
}
