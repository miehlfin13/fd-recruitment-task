using FluentAssertions;
using NUnit.Framework;
using Todo_App.Application.Colours.GetSupportedColours;
using Todo_App.Application.TodoLists.Queries.GetTodos;
using Todo_App.Application.WeatherForecasts.Queries.GetSupportedColour;

namespace Todo_App.Application.IntegrationTests.Colours.Queries;

using static Testing;

public class GetSupportedColoursTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnAllSupportedColours()
    {
        await RunAsDefaultUserAsync();

        var supportedColours = new List<SupportedColour>
        {
            new() { Name = "White", Value = "#FFFFFF" },
            new() { Name = "Red", Value = "#FF5733" },
            new() { Name = "Orange", Value = "#FFC300" },
            new() { Name = "Yellow", Value = "#FFFF66" },
            new() { Name = "Green", Value = "#CCFF99" },
            new() { Name = "Blue", Value = "#6666FF" },
            new() { Name = "Purple", Value = "#9966CC" },
            new() { Name = "Grey", Value = "#999999" }
        };

        var query = new GetSupportedColourQuery();

        var result = await SendAsync(query);

        result.Should().HaveCount(supportedColours.Count);
        result.Should().BeEquivalentTo(supportedColours);
    }

    [Test]
    public async Task ShouldAcceptAnonymousUser()
    {
        var query = new GetTodosQuery();

        var action = () => SendAsync(query);

        await action.Should().NotThrowAsync<UnauthorizedAccessException>();
    }
}
