using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.ProjectTasks
{
    public class SaveProjectTaskCommandValidator : AbstractValidator<SaveProjectTaskCommand>
    {
        public SaveProjectTaskCommandValidator(ApplicationDbContext context)
        {
            // ProjectId must be non-negative
            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("ProjectId must be greater than zero");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters")
                .Custom((s, context) =>
                {
                    // Instance validation logic can go here if needed
                });

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required");
        }
    }
}
