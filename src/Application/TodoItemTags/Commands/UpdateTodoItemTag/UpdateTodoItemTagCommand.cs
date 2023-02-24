using MediatR;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TodoItemTags.Commands.UpdateTodoItemTag;
public class UpdateTodoItemTagCommand : IRequest
{
    public int Id { get; set; }

    public string? Description { get; set; }
}

public class UpdateTodoItemTagCommandHandler : IRequestHandler<UpdateTodoItemTagCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemTagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItemTag), request.Id);
        }

        entity.Title = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
