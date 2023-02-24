using FluentValidation;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TodoItemTags.Commands.UpdateTodoItemTag;
public class UpdateTodoItemTagCommandValidator : AbstractValidator<TodoItemTag>
{
    public UpdateTodoItemTagCommandValidator()
    {
        RuleFor(v => v.Description)
            .MaximumLength(200)
            .NotEmpty();
    }
}
