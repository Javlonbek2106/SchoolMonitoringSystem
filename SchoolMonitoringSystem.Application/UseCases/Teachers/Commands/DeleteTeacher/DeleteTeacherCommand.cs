using AutoMapper;
using MediatR;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Application.Common.Interfaces;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;


public record DeleteTeacherCommand(Guid Id) : IRequest<TeacherDto>;

public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, TeacherDto>
{
    private IApplicationDbContext _dbContext;
    private IMapper _mapper;
    private IDeleteImg _deleteImg;

    public DeleteTeacherCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IDeleteImg deleteImg)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _deleteImg = deleteImg;
    }

    public async Task<TeacherDto> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
        Teacher teacher = FilterIfTeacherExists(request.Id);

        _dbContext.Teachers.Remove(teacher);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TeacherDto>(teacher);
    }

    private Teacher FilterIfTeacherExists(Guid id)
    {
        Teacher? teacher = _dbContext.Teachers.FirstOrDefault(c => c.Id == id);

        if (teacher is null)
        {
            throw new NotFoundException(" There is no teacher with id. ");
        }
        if (teacher.Img is not null)
        {
            _deleteImg.Delete_Img(teacher.Img);
        }

        return teacher;
    }
}

