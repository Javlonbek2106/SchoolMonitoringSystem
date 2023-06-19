using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public record GetStudentQuery(Guid Id) : IRequest<GetStudentsWithGrades>;

public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, GetStudentsWithGrades>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GetStudentQueryHandler(IApplicationDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }


    public async Task<GetStudentsWithGrades> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        GetStudentsWithGrades student = FilterIfStudentExists(request.Id);

        return _mapper.Map<GetStudentsWithGrades>(student);
    }

    private GetStudentsWithGrades FilterIfStudentExists(Guid id)
    {
        Student? student = _dbContext.Students
            .Include(x=>x.Grades)
            .FirstOrDefault(x => x.Id == id);

        GradeDto[] mappedSt = _mapper.Map<GradeDto[]>(student?.Grades);
        GetStudentsWithGrades getAllStudentDto = new()
        {
            FirstName = student?.FirstName,
            LastName = student?.LastName,
            Id = student.Id,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            BirthDate = student.BirthDate,
            StudentRageNumber = student.StudentRageNumber,
            Grades = mappedSt,
            Img = Path.GetRelativePath(_webHostEnvironment.WebRootPath, student?.Img)
        };
        

        if (student is null)
        {
            throw new NotFoundException(" There is on student with this Id. ");
        }

        return getAllStudentDto;
    }



}