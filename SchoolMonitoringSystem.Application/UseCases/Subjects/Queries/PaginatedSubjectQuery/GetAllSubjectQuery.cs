using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public record GetAllSubjectQuery : IRequest<List<SubjectDto>>;

public class GetAllSubjectCommandHandler : IRequestHandler<GetAllSubjectQuery, List<SubjectDto>>
{

    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetAllSubjectCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<SubjectDto>> Handle(GetAllSubjectQuery request, CancellationToken cancellationToken)
    {
        Subject[] orders = await _dbContext.Subjects.Include(x => x.Grades).ToArrayAsync();

        List<SubjectDto> dtos = _mapper.Map<SubjectDto[]>(orders).ToList();

        return dtos;
    }
}
