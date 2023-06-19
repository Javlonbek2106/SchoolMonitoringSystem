using FluentValidation;

namespace SchoolMonitoringSystem.Application.UseCases;

public class CreateGradeCommandValidator : AbstractValidator<CreateGradeCommand>
{
    public CreateGradeCommandValidator()
    {
        RuleFor(x => x.SubjectId)
            .NotEqual(default(Guid))
            .WithMessage(" Guid format is inccorect. ");

        RuleFor(x => x.StudentId)
            .NotEqual(default(Guid)).
            WithMessage(" Guid format is inccorect. ");

        RuleFor(x => x.Ball).GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100)
            .WithMessage(" Grad should be between 0 and 100.  ");
    }
}
