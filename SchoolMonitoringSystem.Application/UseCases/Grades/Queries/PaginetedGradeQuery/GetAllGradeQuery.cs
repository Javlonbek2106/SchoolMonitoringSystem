using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public record GetAllGradeQuery : IRequest<List<GradeDto>>;


public class GetAllGradeCommandHandler : IRequestHandler<GetAllGradeQuery, List<GradeDto>>
{

    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetAllGradeCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<GradeDto>> Handle(GetAllGradeQuery request, CancellationToken cancellationToken)
    {
        Grade[] orders = await _dbContext.Grades.ToArrayAsync();

        List<GradeDto> dtos = _mapper.Map<GradeDto[]>(orders).ToList();
         
        return dtos;
    }
}
 
