using Application.Services;
using Domain.Factory;
using Domain.IRepository;
using Domain.Models;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.TechnologyServiceTests
{
    public class SubmitTestsAsync
    {
        [Fact]
        public async Task SubmitAsync_ShouldThrowArgumentException_WhenTechnologyExists()
        {
            // Arrange
            var factoryDouble = new Mock<ITechnologyFactory>();
            var repositoryDouble = new Mock<ITechnologyRepositoryEF>();

            var description = "Existing Technology";

            repositoryDouble
                .Setup(r => r.IsRepeated(description))
                .ReturnsAsync(true);

            var service = new TechnologyService(factoryDouble.Object, repositoryDouble.Object, null, null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => service.SubmitAsync(description));
            Assert.Equal("Já existe uma tecnologia com este ID.", exception.Message);

            repositoryDouble.Verify(r => r.IsRepeated(description), Times.Once);
            factoryDouble.Verify(f => f.CreateAsync(It.IsAny<string>()), Times.Never);
            repositoryDouble.Verify(r => r.AddAsync(It.IsAny<Technology>()), Times.Never);
            repositoryDouble.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task SubmitAsync_ShouldCreateAddAndSave_WhenTechnologyDoesNotExist()
        {
            // Arrange
            var factoryDouble = new Mock<ITechnologyFactory>();
            var repositoryDouble = new Mock<ITechnologyRepositoryEF>();

            var description = "New Technology";
            var technology = new Technology(Guid.NewGuid(), description);

            repositoryDouble
                .Setup(r => r.IsRepeated(description))
                .ReturnsAsync(false);

            factoryDouble
                .Setup(f => f.CreateAsync(description))
                .ReturnsAsync(technology);

            repositoryDouble
                .Setup(r => r.AddAsync(technology))
                .ReturnsAsync(technology);

            repositoryDouble
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.FromResult(0));

            var service = new TechnologyService(factoryDouble.Object, repositoryDouble.Object, null, null);

            // Act
            await service.SubmitAsync(description);

            // Assert
            repositoryDouble.Verify(r => r.IsRepeated(description), Times.Once);
            factoryDouble.Verify(f => f.CreateAsync(description), Times.Once);
            repositoryDouble.Verify(r => r.AddAsync(technology), Times.Once);
            repositoryDouble.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
