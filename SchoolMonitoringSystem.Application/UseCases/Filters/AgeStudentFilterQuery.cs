using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{
    public record AgeStudentFilterQuery() : IRequest<List<StudentDto>>;
    public class AgeStudentFilterQueryHandler : IRequestHandler<AgeStudentFilterQuery, List<StudentDto>>
    {


        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public AgeStudentFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<StudentDto>> Handle(AgeStudentFilterQuery request, CancellationToken cancellationToken)
        {
            Student[] students = await _dbContext.Students.Include(x => x.Grades).ToArrayAsync();

            var filterAgeStudents = from student in students
                                 where student.BirthDate.AddYears(20) >= DateTime.Now
                                 select student;

            List<StudentDto> studentDtos = _mapper.Map<StudentDto[]>(filterAgeStudents).ToList();

           

            return studentDtos;
        }
    }
}
