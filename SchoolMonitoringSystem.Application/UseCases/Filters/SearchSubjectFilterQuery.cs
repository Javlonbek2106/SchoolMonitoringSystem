using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases.Filters
{
    public class SearchSubjectFilterQuery : IRequest<List<SubjectDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchPattern { get; set; }
    }
    public class SearchSubjectFilterQueryHandler : IRequestHandler<SearchSubjectFilterQuery, List<SubjectDto>>
    {


        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public SearchSubjectFilterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<SubjectDto>> Handle(SearchSubjectFilterQuery request, CancellationToken cancellationToken)
        {
            Subject[] subjects = await _dbContext.Subjects.Include(x => x.Grades).ToArrayAsync();

            var SearchSubjectFilter = from subject in subjects
                                      where subject.SubjectName.Contains(request.SearchPattern, StringComparison.OrdinalIgnoreCase)
                                      select subject;

            List<SubjectDto> subjectDtos = _mapper.Map<SubjectDto[]>(SearchSubjectFilter).ToList();


            return subjectDtos;
        }
    }
}
