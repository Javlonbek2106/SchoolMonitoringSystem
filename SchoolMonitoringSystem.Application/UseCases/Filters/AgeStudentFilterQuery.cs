using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{
	public record AgeStudentFilterQuery(int PageNumber=1, int PageSize = 10) : IRequest<PaginatedList<StudentDto>>;
    public class AgeStudentFilterQueryHandler : IRequestHandler<AgeStudentFilterQuery, PaginatedList<StudentDto>>
    {


        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public AgeStudentFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<StudentDto>> Handle(AgeStudentFilterQuery request, CancellationToken cancellationToken)
        {
            Student[] students = await _dbContext.Students.Include(x => x.Grades).ToArrayAsync();

            var filterAgeStudents = from student in students
                                 where student.BirthDate.AddYears(20) >= DateTime.Now
                                 select student;

            List<StudentDto> studentDtos = _mapper.Map<StudentDto[]>(filterAgeStudents).ToList();

            PaginatedList<StudentDto> paginatedList =
                 PaginatedList<StudentDto>.CreateAsync(
                    studentDtos, request.PageNumber, request.PageSize);

            return paginatedList;
        }
    }
}
