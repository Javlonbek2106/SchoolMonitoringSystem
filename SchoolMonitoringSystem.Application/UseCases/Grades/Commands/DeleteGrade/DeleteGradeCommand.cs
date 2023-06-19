using AutoMapper;
using MediatR;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public record DeleteGradeCommand(Guid Id) : IRequest<GradeDto>;

public class DeleteGradeCommandHandler : IRequestHandler<DeleteGradeCommand, GradeDto>
{
    private IApplicationDbContext _dbContext;
    private IMapper _mapper;

    public DeleteGradeCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GradeDto> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
    {
        Grade grade = FilterIfGradeExists(request.Id);

        _dbContext.Grades.Remove(grade);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GradeDto>(grade);
    }

    private Grade FilterIfGradeExists(Guid id)
    {
        Grade? grade = _dbContext.Grades.FirstOrDefault(c => c.Id == id);

        if (grade is null)
        {
            throw new NotFoundException(" There is no grade with id. ");
        }

        return grade;
    }
}


