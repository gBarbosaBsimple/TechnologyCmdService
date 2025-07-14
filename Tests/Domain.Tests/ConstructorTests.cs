using Domain.Models;
using Xunit;

namespace Domain.Tests;

public class ConstructorTests
{
    [Fact]
    public void WhenCreatingTechnologyWithValidDescription_ThenTechnologyIsCreated()
    {
        // Arrange
        string description = "Continuous integration pipeline";

        // Act
        Technology tech = new Technology(description);

        // Assert
        Assert.NotNull(tech);
        Assert.Equal(description, tech.Description);
        Assert.NotEqual(Guid.Empty, tech.Id);
    }

    [Fact]
    public void WhenCreatingTechnologyWithId_ThenTechnologyIsCreated()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string description = "GitHub Actions";

        // Act
        Technology tech = new Technology(id, description);

        // Assert
        Assert.Equal(id, tech.Id);
        Assert.Equal(description, tech.Description);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")] // >255
    public void WhenCreatingTechnologyWithInvalidDescription_ThenThrowsArgumentException(string? desc)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Technology(desc!));
    }

    [Fact]
    public void WhenUpdatingDescriptionWithValidValue_ThenDescriptionChanges()
    {
        // Arrange
        var tech = new Technology("Old description");
        var newDesc = "New valid description";

        // Act
        tech.UpdateDescription(newDesc);

        // Assert
        Assert.Equal(newDesc, tech.Description);
    }
}
