using FluentValidation;

namespace SchoolMonitoringSystem.Application.UseCases;

public  class UpdateStudentCommandValidation : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidation() 
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage(" Name is required . ");
    }


}
