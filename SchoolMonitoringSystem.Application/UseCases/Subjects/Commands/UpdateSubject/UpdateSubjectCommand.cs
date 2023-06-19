using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public class UpdateSubjectCommand : IRequest<SubjectDto>
{
    public Guid Id { get; set; }
    public string SubjectName { get; set; }

    public Guid TeacherId { get; set; }

}
public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, SubjectDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public UpdateSubjectCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<SubjectDto> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        Subject subject = await FilterIfSubjectExists(request.Id);

        subject.SubjectName = request.SubjectName;
        subject.TeacherId = request.TeacherId;
        _dbContext.Subjects.Update(subject);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SubjectDto>(subject);
    }

    private async Task<Subject> FilterIfSubjectExists(Guid id)
    {
        Subject? subject = await _dbContext.Subjects
            .FirstOrDefaultAsync(x => x.Id == id);

        return subject
            ?? throw new NotFoundException(
                " there is no subject with this id. ");
    }
}
