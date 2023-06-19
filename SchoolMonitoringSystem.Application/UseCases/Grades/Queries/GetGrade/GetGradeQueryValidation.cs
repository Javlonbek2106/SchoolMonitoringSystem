using FluentValidation;

namespace SchoolMonitoringSystem.Application.UseCases;

public class GetGradeQueryValidation :AbstractValidator<GetGradeQuery>
{
    public GetGradeQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual((Guid)default).WithMessage(" invalid Guid Format. ");
    }
}
