using AutoMapper;
using MediatR;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public class UpdateTeacherCommand : IRequest<TeacherDto>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

}

public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, TeacherDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public UpdateTeacherCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<TeacherDto> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {

       Teacher teacher=  FilterIfTeacherExists(request.Id);


        teacher.FirstName = request.FirstName;
        teacher.PhoneNumber = request.PhoneNumber;
        teacher.LastName = request.LastName;
        teacher.BirthDate=request.BirthDate;
        teacher.Email = request.Email;

         _dbContext.Teachers.Update(teacher);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TeacherDto>(teacher);
    }

    private Teacher FilterIfTeacherExists(Guid Id)
    {
        Teacher? teacher = _dbContext.Teachers.
            FirstOrDefault(x => x.Id==Id);

        if (teacher is null)
        {
            throw new NotFoundException(
                " There is no   teacher with this Id . ");
        }

        return teacher;
    }
}
