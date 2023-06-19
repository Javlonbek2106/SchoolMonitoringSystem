using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Application.UseCases;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;


public record GetSubjectQuery(Guid Id) : IRequest<SubjectDto>;

public class GetSubjectQueryHandler : IRequestHandler<GetSubjectQuery, SubjectDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetSubjectQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<SubjectDto> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
    {
        SubjectDto subject = FilterIfSubjectExists(request.Id);

        return _mapper.Map<SubjectDto>(subject);
    }

    private SubjectDto FilterIfSubjectExists(Guid id)
    {
        Subject? subject = _dbContext.Subjects
            .Include(x=>x.Grades)
            .FirstOrDefault(x => x.Id == id);

        GradeDto[] mappedSt = _mapper.Map<GradeDto[]>(subject.Grades);

        SubjectDto getAllSubjectDto = new()
        {
          SubjectName = subject.SubjectName,
          TeacherId=subject.TeacherId,
          Id= subject.Id
        };
        

        if (subject is null)
        {
            throw new NotFoundException(
                " There is on subject with this Id. ");
        }

        return getAllSubjectDto;
    }


}