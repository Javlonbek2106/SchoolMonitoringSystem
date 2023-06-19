using AutoMapper;
using MediatR;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{
    public record BestTeacherFilterQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<TeacherDto>>;
    public class BestTeacherFilterQueryHandler : IRequestHandler<BestTeacherFilterQuery, PaginatedList<TeacherDto>>
    {


        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public BestTeacherFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TeacherDto>> Handle(BestTeacherFilterQuery request, CancellationToken cancellationToken)
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

            PaginatedList<TeacherDto> paginatedList =
                 PaginatedList<TeacherDto>.CreateAsync(
                    dtos, request.PageNumber, request.PageSize);

            return paginatedList;
        }

    }
}


