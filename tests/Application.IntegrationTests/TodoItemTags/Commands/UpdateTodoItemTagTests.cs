using FluentAssertions;
using NUnit.Framework;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.TodoItems.Commands.CreateTodoItem;
using Todo_App.Application.TodoItemTags.Commands.CreateTodoItemTag;
using Todo_App.Application.TodoItemTags.Commands.UpdateTodoItemTag;
using Todo_App.Application.TodoLists.Commands.CreateTodoList;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.IntegrationTests.TodoItemTags.Commands;

using static Testing;

public class UpdateTodoItemTagTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new UpdateTodoItemTagCommand { Id = 99, Description = "New Tag" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateTodoItem()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        var command = new CreateTodoItemTagCommand
        {
            Description = "Sample Tag",
            ItemId = itemId
        };

        var id = await SendAsync(command);

        var tag = await FindAsync<TodoItemTag>(id);

        tag.Should().NotBeNull();
        tag!.Description.Should().Be(command.Description);
        tag.LastModifiedBy.Should().NotBeNull();
        tag.LastModifiedBy.Should().Be(userId);
        tag.LastModified.Should().NotBeNull();
        tag.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
