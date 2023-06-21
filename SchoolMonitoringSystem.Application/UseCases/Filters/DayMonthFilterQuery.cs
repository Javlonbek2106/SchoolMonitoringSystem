using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{
    public record DayMonthFilterQuery() : IRequest<List<StudentDto>>;

	public class DayMonthFilterQueryHandler : IRequestHandler<DayMonthFilterQuery, List<StudentDto>>
	{


		IApplicationDbContext _dbContext;
		IMapper _mapper;

		public DayMonthFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<List<StudentDto>> Handle(DayMonthFilterQuery request, CancellationToken cancellationToken)
		{
			Student[] students = await _dbContext.Students.Include(x => x.Grades).ToArrayAsync();

			var filterDayMonthStudents = from student in students
										 where (student.BirthDate.Day > 12 && student.BirthDate.Month == 8) || (student.BirthDate.Day < 18 && student.BirthDate.Month == 9)
										 select student;

			List<StudentDto> studentDtos = _mapper.Map<StudentDto[]>(filterDayMonthStudents).ToList();

			

			return studentDtos;
		}
	}
}
