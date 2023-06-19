using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Application.Common.Interfaces;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public class UpdateStudentCommand : IRequest<StudentDto>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }
    public IFormFile Img { get; set; }


}
public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, StudentDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;
    ISaveImg _saveImg;
    IDeleteImg _deleteImg;
    IWebHostEnvironment _webHostEnvironment;

    public UpdateStudentCommandHandler(IApplicationDbContext dbContext, IMapper mapper, ISaveImg saveImg, IDeleteImg deleteImg, IWebHostEnvironment webHostEnvironment)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _saveImg = saveImg;
        _deleteImg = deleteImg;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<StudentDto> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        Student student = await FilterIfStudentExists(request.Id);
        if(student.Img is not null)
        {
            _deleteImg.Delete_Img(student.Img);
        }
        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        student.Email = request.Email;
        student.PhoneNumber = request.PhoneNumber;
        student.BirthDate = request.BirthDate;
        student.Img = SaveImage(request.Img);
        _dbContext.Students.Update(student);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StudentDto>(student);
    }

    private async Task<Student> FilterIfStudentExists(Guid id)
    {
        Student? student = await _dbContext.Students
            .FirstOrDefaultAsync(x => x.Id == id);

        return student
            ?? throw new NotFoundException(
                " there is no student with this id. ");
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
