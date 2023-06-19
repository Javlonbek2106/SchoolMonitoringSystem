using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{
	public class SearchTeacherFilterQuery : IRequest<PaginatedList<TeacherDto>>
	{
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
		public string? SearchPattern { get; set; }
	}
	public class SearchTeacherFilterQueryHandler : IRequestHandler<SearchTeacherFilterQuery, PaginatedList<TeacherDto>>
	{


		IApplicationDbContext _dbContext;
		IMapper _mapper;

		public SearchTeacherFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<PaginatedList<TeacherDto>> Handle(SearchTeacherFilterQuery request, CancellationToken cancellationToken)
		{
			Teacher[] teachers = await _dbContext.Teachers.Include(x => x.Subjects).ToArrayAsync();

			var SearchTeacherFilter = from teacher in teachers
									  where teacher.FirstName.Contains(request.SearchPattern, StringComparison.OrdinalIgnoreCase)
									  || teacher.LastName.Contains(request.SearchPattern, StringComparison.OrdinalIgnoreCase)
									  select teacher;

			List<TeacherDto> teacherDtos = _mapper.Map<TeacherDto[]>(SearchTeacherFilter).ToList();

			PaginatedList<TeacherDto> paginatedList =
				 PaginatedList<TeacherDto>.CreateAsync(
					teacherDtos, request.PageNumber, request.PageSize);

			return paginatedList;
		}
	}
}
