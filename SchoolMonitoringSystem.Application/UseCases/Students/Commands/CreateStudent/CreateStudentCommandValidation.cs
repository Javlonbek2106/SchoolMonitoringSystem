using FluentValidation;

namespace SchoolMonitoringSystem.Application.UseCases;

public class CreateStudentCommandValidation : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage(" Name is required . ");
    }
}
