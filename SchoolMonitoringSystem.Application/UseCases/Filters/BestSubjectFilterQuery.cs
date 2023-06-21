using AutoMapper;
using MediatR;
using SchoolMonitoringSystem.Application.Common;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{
    public record BestSubjectFilterQuery : IRequest<List<SubjectDto>>
    {
        public Guid TeacherId { get; set; }
        public decimal MinBall { get; set; }
        public decimal TakeStudentNum { get; set; }

    };


    public class BestSubjectFilterQueryHandler : IRequestHandler<BestSubjectFilterQuery, List<SubjectDto>>
    {


        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public BestSubjectFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<SubjectDto>> Handle(BestSubjectFilterQuery request, CancellationToken cancellationToken)
        {

            var FilteredSubjects = from subject in _dbContext.Subjects
                                   join mark in _dbContext.Grades
                                   on subject.Id equals mark.SubjectId
                                   where mark.Ball > request.MinBall
                                   && subject.TeacherId == request.TeacherId
                                   select subject;

            FilteredSubjects = FilteredSubjects.OrderBy(x => x.SubjectName);

  
            IEnumerable<SubjectDto> result = _mapper.Map<SubjectDto[]>(FilteredSubjects);
            return result.ToList();

        }
    }
}

          


   