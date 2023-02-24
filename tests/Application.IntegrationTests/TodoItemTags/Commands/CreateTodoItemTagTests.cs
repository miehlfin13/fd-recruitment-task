using FluentAssertions;
using NUnit.Framework;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.TodoItems.Commands.CreateTodoItem;
using Todo_App.Application.TodoItemTags.Commands.CreateTodoItemTag;
using Todo_App.Application.TodoLists.Commands.CreateTodoList;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.IntegrationTests.TodoItemTags.Commands;

using static Testing;

public class CreateTodoItemTagTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateTodoItemTagCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireDescription()
    {
        var command = new CreateTodoItemTagCommand
        {
            Description = ""
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldLimitDescriptionTo200()
    {
        var command = new CreateTodoItemTagCommand
        {
            Description = new string('A', 201)
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateTodoItemTag()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var item = new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "Tasks"
        };

        var itemId = await SendAsync(item);

        var command = new CreateTodoItemTagCommand
        {
            Description = "Sample Tag",
            ItemId = itemId
        };

        var id = await SendAsync(command);

        var tag = await FindAsync<TodoItemTag>(id);

        tag.Should().NotBeNull();
        tag!.Description.Should().Be(command.Description);
        tag.CreatedBy.Should().Be(userId);
        tag.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
