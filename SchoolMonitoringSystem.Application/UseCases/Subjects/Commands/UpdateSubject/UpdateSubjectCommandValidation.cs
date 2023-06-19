using FluentValidation;

namespace SchoolMonitoringSystem.Application.UseCases;

public class UpdateSubjectCommandValidation : AbstractValidator<UpdateSubjectCommand>
{
    public UpdateSubjectCommandValidation()
    {
        RuleFor(x => x.SubjectName).NotEmpty().WithMessage(" Name is required . ");
    }


}

