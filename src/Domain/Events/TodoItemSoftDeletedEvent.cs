namespace Todo_App.Domain.Events;
public class TodoItemSoftDeletedEvent : BaseEvent
{
    public TodoItemSoftDeletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
