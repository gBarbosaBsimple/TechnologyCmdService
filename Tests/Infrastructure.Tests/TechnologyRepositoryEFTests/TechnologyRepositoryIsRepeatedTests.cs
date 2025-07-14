using Domain.Interfaces;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Moq;
using Xunit;

namespace Infrastructure.Tests.TechnologyRepositoryTests;

public class TechnologyRepositoryIsRepeatedTests : RepositoryTestBase
{
    [Theory]
    [InlineData("Docker", true)]
    [InlineData("Helm", false)]
    public async Task IsRepeated_ReturnsExpected(string description, bool expected)
    {
        // Arrange – seed DB with one technology "Docker"
        var existing = new TechnologyDataModel { Id = Guid.NewGuid(), Description = "Docker" };
        Context.Set<TechnologyDataModel>().Add(existing);
        await Context.SaveChangesAsync();

        var repo = new TechnologyRepositoryEF(Context, Mapper.Object);

        // Act
        var result = await repo.IsRepeated(description);

        // Assert
        Assert.Equal(expected, result);
    }
}
