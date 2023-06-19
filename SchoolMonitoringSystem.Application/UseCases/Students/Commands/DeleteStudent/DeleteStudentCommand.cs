using AutoMapper;
using MediatR;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Application.Common.Interfaces;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public record DeleteStudentCommand(Guid Id) : IRequest<StudentDto>;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, StudentDto>
{
    private IApplicationDbContext _dbContext;
    private IMapper _mapper;
    private IDeleteImg _deleteImg;

    public DeleteStudentCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IDeleteImg deleteImg)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _deleteImg = deleteImg;
    }

    public async Task<StudentDto> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        Student student = FilterIfStudentExists(request.Id);

        _dbContext.Students.Remove(student);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StudentDto>(student);
    }

    private Student FilterIfStudentExists(Guid id)
    {
        Student? student = _dbContext.Students.FirstOrDefault(c => c.Id == id);

        if (student is null)
        {
            throw new NotFoundException(" There is no student with id. ");
        }
        if (student.Img is not null)
        {
            _deleteImg.Delete_Img(student.Img);
        }

        return student;
    }
}


