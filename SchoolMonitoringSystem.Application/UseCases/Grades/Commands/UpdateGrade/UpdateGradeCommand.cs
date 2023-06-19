using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public class UpdateGradeCommand : IRequest<GradeDto>
{
    public Guid Id { get; set; }
    public Guid SubjectId { get; set; }

    public Guid StudentId { get; set; }

    public decimal Ball { get; set; }

}
public class UpdateGradeCommandHandler : IRequestHandler<UpdateGradeCommand, GradeDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public UpdateGradeCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GradeDto> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
    {
        Grade srade = await FilterIfGradeExists(request.Id);

        srade.Ball = request.Ball;
        _dbContext.Grades.Update(srade);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GradeDto>(srade);
    }

    private async Task<Grade> FilterIfGradeExists(Guid id)
    {
        Grade? srade = await _dbContext.Grades
            .FirstOrDefaultAsync(x => x.Id == id);

        return srade
            ?? throw new NotFoundException(
                " there is no Grade with this id. ");
    }
}
