using MediatR;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;
using Todo_App.Domain.Events;

namespace Todo_App.Application.TodoItemTags.Commands.CreateTodoItemTag;
public class CreateTodoItemTagCommand : IRequest<int>
{
    public int ItemId { get; set; }

    public string? Description { get; set; }
}

public class CreateTodoItemTagCommandHandler : IRequestHandler<CreateTodoItemTagCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemTagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoItemTagCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItemTag
        {
            ItemId = request.ItemId,
            Description = request.Description
        };

        entity.AddDomainEvent(new TodoItemTagCreatedEvent(entity));

        _context.TodoItemTags.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}