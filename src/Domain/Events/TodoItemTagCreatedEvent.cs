namespace Todo_App.Domain.Events;
public class TodoItemTagCreatedEvent : BaseEvent
{
    public TodoItemTagCreatedEvent(TodoItemTag tag)
    {
        Tag = tag;
    }

    public TodoItemTag Tag { get; set; }
}
