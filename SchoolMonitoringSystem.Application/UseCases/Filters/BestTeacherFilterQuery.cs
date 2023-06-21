using AutoMapper;
using MediatR;
using SchoolMonitoringSystem.Application.Common;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{
    public record BestTeacherFilterQuery() : IRequest<List<TeacherDto>>;
    public class BestTeacherFilterQueryHandler : IRequestHandler<BestTeacherFilterQuery, List<TeacherDto>>
    {


        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public BestTeacherFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<TeacherDto>> Handle(BestTeacherFilterQuery request, CancellationToken cancellationToken)
        {
            var teachers = from mark in _dbContext.Grades
                           join subject in _dbContext.Subjects
                           on mark.SubjectId equals subject.Id
                           join teacher in _dbContext.Teachers
                           on subject.TeacherId equals teacher.Id
                           where mark.Ball > 97
                           select teacher;

            var ListTeachers = teachers.Distinct().ToList();

            List<TeacherDto> dtos = _mapper.Map<TeacherDto[]>(ListTeachers).ToList();

       

            return dtos;
        }

    }
}


