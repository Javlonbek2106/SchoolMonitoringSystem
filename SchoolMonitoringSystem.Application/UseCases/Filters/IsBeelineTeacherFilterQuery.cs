using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{

	public record IsBeelineTeacherFilterQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<TeacherDto>>;
	public class IsBeelineTeacherFilterQueryHandler : IRequestHandler<IsBeelineTeacherFilterQuery, PaginatedList<TeacherDto>>
	{


		IApplicationDbContext _dbContext;
		IMapper _mapper;

		public IsBeelineTeacherFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<PaginatedList<TeacherDto>> Handle(IsBeelineTeacherFilterQuery request, CancellationToken cancellationToken)
		{
			Teacher[] teachers = await _dbContext.Teachers.Include(x => x.Subjects).ToArrayAsync();

			var IsBeelineTeacherFilter = from teacher in teachers
										 where teacher.PhoneNumber.Substring(4, 2) == "90" || teacher.PhoneNumber.Substring(5, 2) == "91"
										 select teacher;

			List<TeacherDto> teacherDtos = _mapper.Map<TeacherDto[]>(IsBeelineTeacherFilter).ToList();

			PaginatedList<TeacherDto> paginatedList =
				 PaginatedList<TeacherDto>.CreateAsync(
					teacherDtos, request.PageNumber, request.PageSize);

			return paginatedList;
		}
	}


}
