using Todo_App.Application.Common.Mappings;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TodoLists.Queries.GetTodos;
public class TodoItemTagDto : IMapFrom<TodoItemTag>
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public string? Description { get; set; }
}
