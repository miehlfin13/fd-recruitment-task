using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.TodoItems.Commands.DeleteTodoItem;
using Todo_App.Domain.Entities;
using Todo_App.Domain.Events;

namespace Todo_App.Application.TodoItemTags.Commands.DeleteTodoItemTag;

public record DeleteTodoItemTagCommand(int Id) : IRequest;

public class DeleteTodoItemTagCommandHandler : IRequestHandler<DeleteTodoItemTagCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemTagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTodoItemTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItemTags
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItemTag), request.Id);
        }

        _context.TodoItemTags.Remove(entity);

        entity.AddDomainEvent(new TodoItemTagDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
