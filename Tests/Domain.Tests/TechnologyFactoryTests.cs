using Domain.Factory;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests.TechnologyDomainTests;

public class TechnologyFactoryTests
{
    [Fact]
    public async Task WhenCreatingTechnologyAndDescriptionIsUnique_ThenTechnologyIsCreated()
    {
        // Arrange
        var repoMock = new Mock<ITechnologyRepositoryEF>();
        repoMock.Setup(r => r.IsRepeated(It.IsAny<string>())).ReturnsAsync(false);

        var factory = new TechnologyFactory(repoMock.Object);
        var description = "Kafka Streams";

        // Act
        var tech = await factory.CreateAsync(description);

        // Assert
        Assert.NotNull(tech);
        Assert.Equal(description, tech.Description);
        repoMock.Verify(r => r.IsRepeated(description), Times.Once);
    }

    [Fact]
    public async Task WhenCreatingTechnologyAndDescriptionAlreadyExists_ThenThrowsArgumentException()
    {
        // Arrange
        var repoMock = new Mock<ITechnologyRepositoryEF>();
        repoMock.Setup(r => r.IsRepeated(It.IsAny<string>())).ReturnsAsync(true);

        var factory = new TechnologyFactory(repoMock.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => factory.CreateAsync("Duplicate tech"));
        Assert.Equal("Já existe uma tecnologia .", ex.Message);
    }

    [Fact]
    public void WhenCreatingTechnologyFromVisitor_ThenTechnologyIsCreated()
    {
        // Arrange
        var repoMock = new Mock<ITechnologyRepositoryEF>();
        var factory = new TechnologyFactory(repoMock.Object);

        var visitorMock = new Mock<ITechnologyVisitor>();
        visitorMock.Setup(v => v.Id).Returns(Guid.NewGuid());
        visitorMock.Setup(v => v.Description).Returns("Azure Functions");

        // Act
        var tech = factory.Create(visitorMock.Object);

        // Assert
        Assert.Equal(visitorMock.Object.Id, tech.Id);
        Assert.Equal(visitorMock.Object.Description, tech.Description);
    }
}
