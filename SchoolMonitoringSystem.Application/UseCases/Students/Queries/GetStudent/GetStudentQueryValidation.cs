using FluentValidation;

namespace SchoolMonitoringSystem.Application.UseCases;

public class GetStudentQueryValidation : AbstractValidator<GetStudentsWithGrades>
{
    public GetStudentQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual((Guid)default).WithMessage(" invalid Guid Format. ");
    }
}
