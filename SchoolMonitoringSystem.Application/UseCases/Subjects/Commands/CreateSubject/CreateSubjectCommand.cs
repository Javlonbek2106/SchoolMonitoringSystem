using AutoMapper;
using MediatR;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public class CreateSubjectCommand : IRequest<SubjectDto>
{
    public string SubjectName { get; set; }

    public Guid TeacherId { get; set; }

}
public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, SubjectDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public CreateSubjectCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<SubjectDto> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {

        FilterIfSubjectExists(request.SubjectName);

        Subject subject = new ()
        {
        SubjectName=request.SubjectName,
        TeacherId=request.TeacherId
        };

        await  _dbContext.Subjects.AddAsync(subject);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SubjectDto>(subject);
    }

    private void FilterIfSubjectExists(string? SubjectName)
    {
        Subject? subject = _dbContext.Subjects.FirstOrDefault(x => x.SubjectName==SubjectName);

        if (subject is not null)
        {
            throw new AlreadyExistsException(" There is a  subject with this name. Subject should be unique.  ");
        }
    }
}
