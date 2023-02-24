using FluentValidation;

namespace Todo_App.Application.TodoItemTags.Commands.CreateTodoItemTag;

public class CreateTodoItemTagCommandValidator : AbstractValidator<CreateTodoItemTagCommand>
{
    public CreateTodoItemTagCommandValidator()
    {
        RuleFor(v => v.Description)
            .MaximumLength(200)
            .NotEmpty();
    }
}
