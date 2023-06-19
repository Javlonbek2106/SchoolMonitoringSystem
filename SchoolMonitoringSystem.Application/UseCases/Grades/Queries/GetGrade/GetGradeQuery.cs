using AutoMapper;
using MediatR;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public  record GetGradeQuery(Guid Id) : IRequest<GradeDto>;

public class GetGradeQueryHandler : IRequestHandler<GetGradeQuery, GradeDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetGradeQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<GradeDto> Handle(GetGradeQuery request, CancellationToken cancellationToken)
    {
        Grade grade = FilterIfGradeExists(request.Id);

          return  _mapper.Map<GradeDto>(grade);
    }

    private Grade FilterIfGradeExists(Guid id)
    {
        Grade? grade = _dbContext.Grades.Find(id);

        if (grade is null)
        {
            throw new NotFoundException(" There is no grade with this Id. ");
        }

        return grade;
    }
}

