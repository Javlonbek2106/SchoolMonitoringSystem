using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{
	public record DayMonthFilterQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<StudentDto>>;

	public class DayMonthFilterQueryHandler : IRequestHandler<DayMonthFilterQuery, PaginatedList<StudentDto>>
	{


		IApplicationDbContext _dbContext;
		IMapper _mapper;

		public DayMonthFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<PaginatedList<StudentDto>> Handle(DayMonthFilterQuery request, CancellationToken cancellationToken)
		{
			Student[] students = await _dbContext.Students.Include(x => x.Grades).ToArrayAsync();

			var filterDayMonthStudents = from student in students
										 where (student.BirthDate.Day > 12 && student.BirthDate.Month == 8) || (student.BirthDate.Day < 18 && student.BirthDate.Month == 9)
										 select student;

			List<StudentDto> studentDtos = _mapper.Map<StudentDto[]>(filterDayMonthStudents).ToList();

			PaginatedList<StudentDto> paginatedList =
				 PaginatedList<StudentDto>.CreateAsync(
					studentDtos, request.PageNumber, request.PageSize);

			return paginatedList;
		}
	}
}
