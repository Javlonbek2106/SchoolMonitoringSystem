using FluentValidation;

namespace SchoolMonitoringSystem.Application.UseCases;

public class GetSubjectQueryValidation : AbstractValidator<SubjectDto>
{
    public GetSubjectQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual((Guid)default).WithMessage(" invalid Guid Format. ");
    }
}
