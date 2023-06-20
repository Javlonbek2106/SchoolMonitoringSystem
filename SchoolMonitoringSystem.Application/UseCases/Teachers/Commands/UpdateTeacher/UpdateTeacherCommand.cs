using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Application.Common.Interfaces;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public class UpdateTeacherCommand : IRequest<TeacherDto>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }
    public IFormFile Img { get; set; }


}

public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, TeacherDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;
    ISaveImg _saveImg;
    IDeleteImg _deleteImg;
    IWebHostEnvironment _webHostEnvironment;

    public UpdateTeacherCommandHandler(IApplicationDbContext dbContext, IMapper mapper, ISaveImg saveImg, IDeleteImg deleteImg, IWebHostEnvironment webHostEnvironment)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _saveImg = saveImg;
        _deleteImg = deleteImg;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<TeacherDto> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {

        Teacher teacher = FilterIfTeacherExists(request.Id);
        if (teacher.Img is not null)
        {
            _deleteImg.Delete_Img(teacher.Img);
        }

        teacher.FirstName = request.FirstName;
        teacher.PhoneNumber = request.PhoneNumber;
        teacher.LastName = request.LastName;
        teacher.BirthDate = request.BirthDate;
        teacher.Email = request.Email;
        teacher.Img = SaveImage(request.Img);

        _dbContext.Teachers.Update(teacher);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TeacherDto>(teacher);
    }

    private Teacher FilterIfTeacherExists(Guid Id)
    {
        Teacher? teacher = _dbContext.Teachers.
            FirstOrDefault(x => x.Id == Id);

        if (teacher is null)
        {
            throw new NotFoundException(
                " There is no   teacher with this Id . ");
        }

        return teacher;
    }
    private string SaveImage(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            // Image fayli mavjud emas yoki bo'sh
            return string.Empty;
        }

        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            imageFile.CopyTo(fileStream);
        }
        return filePath;
    }
}
