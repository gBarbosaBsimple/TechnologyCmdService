using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Moq;
using Xunit;

namespace Infrastructure.Tests.TechnologyRepositoryTests;

public class TechnologyRepositoryUpdateTechnologyTests : RepositoryTestBase
{
    [Fact]
    public async Task UpdateTechnology_WhenExists_ReturnsUpdated()
    {
        // Arrange: seed one tech
        var id = Guid.NewGuid();
        var tech = new TechnologyDataModel { Id = id, Description = "Old" };
        Context.Set<TechnologyDataModel>().Add(tech);
        await Context.SaveChangesAsync();

        var updatedEntity = new Mock<ITechnology>();
        updatedEntity.Setup(t => t.Id).Returns(id);

        Mapper.Setup(m => m.Map<TechnologyDataModel, Technology>(It.IsAny<TechnologyDataModel>()))
              .Returns(new Technology(id, tech.Description));

        var repo = new TechnologyRepositoryEF(Context, Mapper.Object);

        // Act
        var result = await repo.UpdateTechnology(updatedEntity.Object);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result!.Id);
    }

    [Fact]
    public async Task UpdateTechnology_WhenNotExists_ReturnsNull()
    {
        // Arrange
        var mockTech = new Mock<ITechnology>();
        mockTech.Setup(t => t.Id).Returns(Guid.NewGuid());

        var repo = new TechnologyRepositoryEF(Context, Mapper.Object);

        // Act
        var result = await repo.UpdateTechnology(mockTech.Object);

        // Assert
        Assert.Null(result);
    }
}
