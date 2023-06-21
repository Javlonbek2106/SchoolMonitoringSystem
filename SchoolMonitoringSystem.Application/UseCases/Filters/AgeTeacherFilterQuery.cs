using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{
    public record AgeTeacherFilterQuery() : IRequest<List<TeacherDto>>;
	public class AgeTeacherFilterQueryHandler : IRequestHandler<AgeTeacherFilterQuery, List<TeacherDto>>
	{


		IApplicationDbContext _dbContext;
		IMapper _mapper;

		public AgeTeacherFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<List<TeacherDto>> Handle(AgeTeacherFilterQuery request, CancellationToken cancellationToken)
		{
			Teacher[] teachers = await _dbContext.Teachers.Include(x => x.Subjects).ToArrayAsync();

			var filterAgeTeachers = from teacher in teachers
									where teacher.BirthDate.AddYears(55) <= DateTime.Now
									select teacher;

			List<TeacherDto> teacherDtos = _mapper.Map<TeacherDto[]>(filterAgeTeachers).ToList();

			

			return teacherDtos;
		}
	}
}
