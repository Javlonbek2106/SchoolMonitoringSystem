using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;


public record GetAllTeacherQuery : IRequest<List<TeacherDto>>;


public class GetAllTeacherQueryHandler : IRequestHandler<GetAllTeacherQuery, List<TeacherDto>>
{

    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetAllTeacherQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<TeacherDto>> Handle(GetAllTeacherQuery request, CancellationToken cancellationToken)
    {
        Teacher[] orders = await _dbContext.Teachers.ToArrayAsync();

        List<TeacherDto> dtos = _mapper.Map<TeacherDto[]>(orders).ToList();

        return dtos;
    }
}
