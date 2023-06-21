using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{

    public record IsBeelineStudentFilterQuery() : IRequest<List<StudentDto>>;
		public class IsBeelineStudentFilterQueryHandler : IRequestHandler<IsBeelineStudentFilterQuery, List<StudentDto>>
		{


			IApplicationDbContext _dbContext;
			IMapper _mapper;

			public IsBeelineStudentFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
			{
				_dbContext = dbContext;
				_mapper = mapper;
			}

			public async Task<List<StudentDto>> Handle(IsBeelineStudentFilterQuery request, CancellationToken cancellationToken)
			{
				Student[] students = await _dbContext.Students.Include(x => x.Grades).ToArrayAsync();

				var IsBeelineStudentFilter = from student in students
										where student.PhoneNumber.Substring(4,2)=="90"|| student.PhoneNumber.Substring(5, 2) == "91"
										select student;

				List<StudentDto> studentDtos = _mapper.Map<StudentDto[]>(IsBeelineStudentFilter).ToList();

			

				return studentDtos;
			}
		}
}
