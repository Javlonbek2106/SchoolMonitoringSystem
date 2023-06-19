using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{
	public class SearchStudentFilterQuery : IRequest<PaginatedList<StudentDto>>
	{
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
		public string? SearchPattern { get; set; }
	}
	public class SearchStudentFilterQueryHandler : IRequestHandler<SearchStudentFilterQuery, PaginatedList<StudentDto>>
	{


		IApplicationDbContext _dbContext;
		IMapper _mapper;

		public SearchStudentFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<PaginatedList<StudentDto>> Handle(SearchStudentFilterQuery request, CancellationToken cancellationToken)
		{
			Student[] students = await _dbContext.Students.Include(x => x.Grades).ToArrayAsync();

			var SearchStudentFilter = from student in students
									  where student.FirstName.Contains(request.SearchPattern, StringComparison.OrdinalIgnoreCase)
									  || student.LastName.Contains(request.SearchPattern, StringComparison.OrdinalIgnoreCase)
									  select student;

			List<StudentDto> studentDtos = _mapper.Map<StudentDto[]>(SearchStudentFilter).ToList();

			PaginatedList<StudentDto> paginatedList =
				 PaginatedList<StudentDto>.CreateAsync(
					studentDtos, request.PageNumber, request.PageSize);

			return paginatedList;
		}
	}
}
