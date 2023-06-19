using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Application.Common.Interfaces;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public class CreateStudentCommand : IRequest<StudentDto>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }
    //[ValidateImage(5 * 1024 * 1024, ".jpg", ".jpeg", ".jfif")]
    public IFormFile Img { get; set; }

}
public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentDto>
{
    private readonly ISaveImg _saveImg;
    IApplicationDbContext _dbContext;
    IMapper _mapper;
    IWebHostEnvironment _webHostEnvironment;

    public CreateStudentCommandHandler(ISaveImg saveImg, IApplicationDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _saveImg = saveImg;
        _dbContext = dbContext;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {

        FilterIfStudentExists(request.PhoneNumber);
       
        Student student = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            BirthDate = request.BirthDate,
            Img = SaveImage(request.Img)
        };

        await _dbContext.Students.AddAsync(student);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StudentDto>(student);
    }

    private void FilterIfStudentExists(string? PhoneNumber)
    {
        Student? student = _dbContext.Students.FirstOrDefault(x => x.PhoneNumber == PhoneNumber);

        if (student is not null)
        {
            throw new AlreadyExistsException(" There is a  student with this phonenumber. Student should be unique.  ");
        }
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
