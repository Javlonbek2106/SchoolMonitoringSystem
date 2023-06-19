using FluentValidation;

namespace SchoolMonitoringSystem.Application.UseCases;

public class GetTeacherQueryValidation : AbstractValidator<GetTeacherQuery>
{
    public GetTeacherQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual((Guid)default).WithMessage(" invalid Guid Format. ");
    }
}

