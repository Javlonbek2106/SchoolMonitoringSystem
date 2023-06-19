using AutoMapper;
using MediatR;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Application.UseCases;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application;

public class CreateGradeCommand : IRequest<GradeDto>
{
    public Guid SubjectId { get; set; }

    public Guid StudentId { get; set; }

    public decimal Ball { get; set; }

}
public class CreateGradeCommandHandler : IRequestHandler<CreateGradeCommand, GradeDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public CreateGradeCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GradeDto> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
    {

        FilterIfGradeExists(request.SubjectId, request.StudentId);

        Grade student = new()
        {
        SubjectId = request.SubjectId,
        StudentId=request.StudentId,
        Ball =request.Ball
        };

        await _dbContext.Grades.AddAsync(student);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GradeDto>(student);
    }

    private void FilterIfGradeExists(Guid subjectId, Guid StudentId)
    {
        Grade? student = _dbContext.Grades.FirstOrDefault(
            x => x.StudentId == StudentId && x.SubjectId == subjectId);

        if (student is not null)
        {
            throw new AlreadyExistsException(
                " This student has alreadt taken grate for that subject.  ");
        }
    }
}
