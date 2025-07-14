using AutoMapper;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Infrastructure.Tests.TechnologyRepositoryTests;

public class TechnologyRepositoryGetByIdAsyncTests : RepositoryTestBase
{
    [Fact]
    public async Task WhenSearchingExistingId_ReturnsTechnology()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tech = new TechnologyDataModel { Id = id, Description = "Kubernetes" };
        Context.Set<TechnologyDataModel>().Add(tech);
        await Context.SaveChangesAsync();

        Mapper.Setup(m => m.Map<TechnologyDataModel, Technology>(It.Is<TechnologyDataModel>(d => d.Id == id)))
              .Returns(new Technology(id, tech.Description));

        var repo = new TechnologyRepositoryEF(Context, Mapper.Object);

        // Act
        var result = await repo.GetByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result!.Id);
        Assert.Equal(tech.Description, result.Description);
    }

    [Fact]
    public async Task WhenSearchingNonExistingId_ReturnsNull()
    {
        // Arrange
        var repo = new TechnologyRepositoryEF(Context, Mapper.Object);

        // Act
        var result = await repo.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }
}
