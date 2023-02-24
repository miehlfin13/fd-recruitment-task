namespace Todo_App.Domain.Events;
public class TodoItemTagDeletedEvent : BaseEvent
{
    public TodoItemTagDeletedEvent(TodoItemTag tag)
    {
        Tag = tag;
    }

    public TodoItemTag Tag { get; }
}
