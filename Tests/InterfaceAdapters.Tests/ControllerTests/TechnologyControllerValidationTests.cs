/* using System.Net;
using Application.DTO;
using Xunit;
using InterfaceAdapters.Tests;
using Tests.InterfaceAdapters.Tests;

namespace InterfaceAdapters.Tests.ControllerTests;

public class TechnologyControllerTests : IntegrationTestBase, IClassFixture<IntegrationTestsWebApplicationFactory<Program>>
{
    public TechnologyControllerTests(IntegrationTestsWebApplicationFactory<Program> factory)
        : base(factory.CreateClient()) { }

    [Fact]
    public async Task CreateTechnology_Returns201Created()
    {
        // Arrange – descrição válida (entre 5 e 100 chars)
        var dto = new AddTechnologyDTO { Description = "Artificial Intelligence" };

        // Act
        var response = await PostAsync("/api/technologies", dto);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        var techDto = System.Text.Json.JsonSerializer.Deserialize<TechnologyDTO>(body, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(techDto);
        Assert.Equal(dto.Description, techDto!.Description);
    }
 */
/* [Theory]
[InlineData("", "Description is required.")]
[InlineData("AI", "Description must have between 5 and 100 characters.")]
[InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin ultricies erat nec lorem.",
            "Description must have between 5 and 100 characters.")]
public async Task CreateTechnology_WithInvalidDescription_Returns400BadRequest(string invalidDescription, string expectedMessage)
{
    // Arrange
    var dto = new AddTechnologyDTO { Description = invalidDescription };

    // Act
    var response = await PostAsync("/api/technologies", dto);

    // Assert – status code 400
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    // Assert – mensagem esperada no corpo
    var body = await response.Content.ReadAsStringAsync();
    Assert.Contains(expectedMessage, body);
} */
/* }
 */