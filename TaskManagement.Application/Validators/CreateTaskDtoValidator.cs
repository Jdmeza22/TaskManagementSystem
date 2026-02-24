using FluentValidation;
using TaskManagement.Application.Dtos;

public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
{
    public CreateTaskDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100);

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required")
             .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID");
    }
}