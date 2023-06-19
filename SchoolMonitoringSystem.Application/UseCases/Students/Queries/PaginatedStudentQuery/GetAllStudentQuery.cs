using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public record GetAllStudentQuery : IRequest<List<StudentDto>>;

public class GetAllStudentCommandHandler : IRequestHandler<GetAllStudentQuery, List<StudentDto>>
{

    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetAllStudentCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<StudentDto>> Handle(GetAllStudentQuery request, CancellationToken cancellationToken)
    {
        Student[] students = await _dbContext.Students
                     .Include(x => x.Grades)
                     .ToArrayAsync();


        List<StudentDto> dtos = _mapper.Map<StudentDto[]>(students).ToList();
        return dtos;
    }
}
