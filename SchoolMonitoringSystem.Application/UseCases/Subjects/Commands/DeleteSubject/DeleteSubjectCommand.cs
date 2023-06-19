using AutoMapper;
using MediatR;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public record DeleteSubjectCommand(Guid Id) : IRequest<SubjectDto>;

public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, SubjectDto>
{
    private IApplicationDbContext _dbContext;
    private IMapper _mapper;

    public DeleteSubjectCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<SubjectDto> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        Subject subject = FilterIfSubjectExists(request.Id);

        _dbContext.Subjects.Remove(subject);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SubjectDto>(subject);
    }

    private Subject FilterIfSubjectExists(Guid id)
    {
        Subject? subject = _dbContext.Subjects.FirstOrDefault(c => c.Id == id);

        if (subject is null)
        {
            throw new NotFoundException(
                " There is no subject with id. ");
        }

        return subject;
    }
}


