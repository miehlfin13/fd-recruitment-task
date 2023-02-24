using FluentAssertions;
using NUnit.Framework;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.TodoItems.Commands.CreateTodoItem;
using Todo_App.Application.TodoItems.Commands.DeleteTodoItem;
using Todo_App.Application.TodoItemTags.Commands.CreateTodoItemTag;
using Todo_App.Application.TodoItemTags.Commands.DeleteTodoItemTag;
using Todo_App.Application.TodoLists.Commands.CreateTodoList;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.IntegrationTests.TodoItemTags.Commands;

using static Testing;

public class DeleteTodoItemTagTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new DeleteTodoItemCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoItem()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        var id = await SendAsync(new CreateTodoItemTagCommand
        {
            Description = "Sample Tag",
            ItemId = itemId
        });

        await SendAsync(new DeleteTodoItemTagCommand(id));

        var tag = await FindAsync<TodoItemTag>(id);

        tag.Should().BeNull();
    }
}
