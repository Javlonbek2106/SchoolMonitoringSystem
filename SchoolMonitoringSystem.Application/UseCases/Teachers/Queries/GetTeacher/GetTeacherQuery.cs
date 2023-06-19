using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;


public record GetTeacherQuery(Guid Id) : IRequest<GetTeacherWithSubjectsDto>;

public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, GetTeacherWithSubjectsDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GetTeacherQueryHandler(IApplicationDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }


    public async Task<GetTeacherWithSubjectsDto> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
    {
        Teacher teacher = FilterIfTeacherExists(request.Id);

        return _mapper.Map<GetTeacherWithSubjectsDto>(teacher);
    }

    private Teacher FilterIfTeacherExists(Guid id)
    {
        Teacher? teacher = _dbContext.Teachers.Include(x=>x.Subjects).FirstOrDefault(x => x.Id == id);
        teacher.Img = Path.GetRelativePath(_webHostEnvironment.WebRootPath, teacher?.Img);
        
        if (teacher is null)
        {
            throw new NotFoundException(" There is no teacher with this Id. ");
        }

        return teacher;
    }
}


