using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{

		public record IsBeelineStudentFilterQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<StudentDto>>;
		public class IsBeelineStudentFilterQueryHandler : IRequestHandler<IsBeelineStudentFilterQuery, PaginatedList<StudentDto>>
		{


			IApplicationDbContext _dbContext;
			IMapper _mapper;

			public IsBeelineStudentFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
			{
				_dbContext = dbContext;
				_mapper = mapper;
			}

			public async Task<PaginatedList<StudentDto>> Handle(IsBeelineStudentFilterQuery request, CancellationToken cancellationToken)
			{
				Student[] students = await _dbContext.Students.Include(x => x.Grades).ToArrayAsync();

				var IsBeelineStudentFilter = from student in students
										where student.PhoneNumber.Substring(4,2)=="90"|| student.PhoneNumber.Substring(5, 2) == "91"
										select student;

				List<StudentDto> studentDtos = _mapper.Map<StudentDto[]>(IsBeelineStudentFilter).ToList();

				PaginatedList<StudentDto> paginatedList =
					 PaginatedList<StudentDto>.CreateAsync(
						studentDtos, request.PageNumber, request.PageSize);

				return paginatedList;
			}
		}
}
