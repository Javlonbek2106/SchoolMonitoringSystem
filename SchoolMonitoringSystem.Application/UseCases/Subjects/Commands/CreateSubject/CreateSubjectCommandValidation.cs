using FluentValidation;

namespace SchoolMonitoringSystem.Application.UseCases;

public class CreateSubjectCommandValidation : AbstractValidator<CreateSubjectCommand>
{
    public CreateSubjectCommandValidation()
    {
        RuleFor(x => x.SubjectName).NotEmpty().WithMessage(" Name is required . ");
    }
}
