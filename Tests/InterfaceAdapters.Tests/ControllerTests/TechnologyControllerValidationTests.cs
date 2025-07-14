/* using System.Net;
using Application.DTO;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace InterfaceAdapters.Tests.ControllerTests;

public class TechnologyControllerValidationTests : IntegrationTestBase, IClassFixture<IntegrationTestsWebApplicationFactory<Program>>
{
    public TechnologyControllerValidationTests(IntegrationTestsWebApplicationFactory<Program> factory)
        : base(factory.CreateClient()) { }

    [Theory]
    [InlineData("", "Description is required.")]
    [InlineData("AI", "Description must have between 5 and 100 characters.")]
    [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin ultricies erat nec lorem.", "Description must have between 5 and 100 characters.")]
    public async Task CreateTechnology_WithInvalidDescription_Returns400BadRequest(string invalidDescription, string expectedMessage)
    {
        // Arrange
        var dto = new AddTechnologyDTO { Description = invalidDescription };

        // Act
        var response = await PostAsync("/api/technologies", dto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        Assert.Contains(expectedMessage, body);
    }
}
 */