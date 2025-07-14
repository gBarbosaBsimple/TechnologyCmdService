/* using Application.DTO;
using Application.IPublisher;
using Application.IServices;
using Application.Services;
using AutoMapper;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.TechnologyServiceTests
{
    public class AddTechnologyTestsAsync
    {
        [Fact]
        public async Task AddTechnologyAsync_ShouldAddTechnologyAndPublishMessage_WhenValid()
        {
            // Arrange
            var factoryMock = new Mock<ITechnologyFactory>();
            var repositoryMock = new Mock<ITechnologyRepositoryEF>();
            var publisherMock = new Mock<IMessagePublisher>();
            var mapperMock = new Mock<IMapper>();

            var description = "New Technology";
            var id = Guid.NewGuid();
            var technology = new Technology(id, description);

            factoryMock
                .Setup(f => f.CreateAsync(description))
                .ReturnsAsync(technology);

            repositoryMock
                .Setup(r => r.AddAsync(technology))
                .ReturnsAsync(technology);

            publisherMock
                .Setup(p => p.PublishTechnologyCreatedAsync(id, description))
                .Returns(Task.CompletedTask);

            var dto = new TechnologyDTO { Id = id, Description = description };

            mapperMock
                .Setup(m => m.Map<TechnologyDTO>(technology))
                .Returns(dto);

            var service = new TechnologyService(factoryMock.Object, repositoryMock.Object, mapperMock.Object, publisherMock.Object);

            var addDto = new AddTechnologyDTO { Description = description };

            // Act
            var result = await service.AddTechnologyAsync(addDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(description, result.Value.Description);
            Assert.Equal(id, result.Value.Id);

            factoryMock.Verify(f => f.CreateAsync(description), Times.Once);
            repositoryMock.Verify(r => r.AddAsync(technology), Times.Once);
            publisherMock.Verify(p => p.PublishTechnologyCreatedAsync(id, description), Times.Once);
            mapperMock.Verify(m => m.Map<TechnologyDTO>(technology), Times.Once);
        }

        [Fact]
        public async Task AddTechnologyAsync_ShouldReturnBadRequestError_WhenArgumentExceptionThrown()
        {
            // Arrange
            var factoryMock = new Mock<ITechnologyFactory>();
            var repositoryMock = new Mock<ITechnologyRepositoryEF>();
            var publisherMock = new Mock<IMessagePublisher>();
            var mapperMock = new Mock<IMapper>();

            var description = "Invalid Tech";
            var exceptionMessage = "Invalid argument";

            factoryMock
                .Setup(f => f.CreateAsync(description))
                .ThrowsAsync(new ArgumentException(exceptionMessage));

            var service = new TechnologyService(factoryMock.Object, repositoryMock.Object, mapperMock.Object, publisherMock.Object);
            var addDto = new AddTechnologyDTO { Description = description };

            // Act
            var result = await service.AddTechnologyAsync(addDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(exceptionMessage, result.Error?.Message);

            factoryMock.Verify(f => f.CreateAsync(description), Times.Once);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Technology>()), Times.Never);
            publisherMock.Verify(p => p.PublishTechnologyCreatedAsync(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
            mapperMock.Verify(m => m.Map<TechnologyDTO>(It.IsAny<Technology>()), Times.Never);
        }


        [Fact]
        public async Task AddTechnologyAsync_ShouldReturnBadRequestError_WhenGenericExceptionThrown()
        {
            // Arrange
            var factoryMock = new Mock<ITechnologyFactory>();
            var repositoryMock = new Mock<ITechnologyRepositoryEF>();
            var publisherMock = new Mock<IMessagePublisher>();
            var mapperMock = new Mock<IMapper>();

            var description = "Tech";
            var exceptionMessage = "Something went wrong";

            factoryMock
                .Setup(f => f.CreateAsync(description))
                .ThrowsAsync(new Exception(exceptionMessage));

            var service = new TechnologyService(factoryMock.Object, repositoryMock.Object, mapperMock.Object, publisherMock.Object);
            var addDto = new AddTechnologyDTO { Description = description };

            // Act
            var result = await service.AddTechnologyAsync(addDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(exceptionMessage, result.Error?.Message);

            factoryMock.Verify(f => f.CreateAsync(description), Times.Once);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Technology>()), Times.Never);
            publisherMock.Verify(p => p.PublishTechnologyCreatedAsync(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
            mapperMock.Verify(m => m.Map<TechnologyDTO>(It.IsAny<Technology>()), Times.Never);
        }
    }
}
 */