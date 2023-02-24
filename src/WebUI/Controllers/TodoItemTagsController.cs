using Microsoft.AspNetCore.Mvc;
using Todo_App.Application.Common.Models;
using Todo_App.Application.TodoItemTags.Commands.CreateTodoItemTag;
using Todo_App.Application.TodoItemTags.Commands.DeleteTodoItemTag;
using Todo_App.Application.TodoItemTags.Commands.UpdateTodoItemTag;

namespace Todo_App.WebUI.Controllers;

public class TodoItemTagTagsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTodoItemTagCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateTodoItemTagCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTodoItemTagCommand(id));

        return NoContent();
    }
}
