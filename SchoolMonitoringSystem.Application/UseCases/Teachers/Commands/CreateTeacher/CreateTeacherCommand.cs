using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Exceptions;
using SchoolMonitoringSystem.Application.Common.Interfaces;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.UseCases;

public class CreateTeacherCommand  : IRequest<TeacherDto>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }
    //[ValidateImage(5 * 1024 * 1024, ".jpg", ".jpeg", ".jfif")]
    public IFormFile Img { get; set; }


}
public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, TeacherDto>
{
    private readonly ISaveImg _saveImg;
    IApplicationDbContext _dbContext;
    IMapper _mapper;
    IWebHostEnvironment _webHostEnvironment;

    public CreateTeacherCommandHandler(ISaveImg saveImg, IApplicationDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _saveImg = saveImg;
        _dbContext = dbContext;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<TeacherDto> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {

        FilterIfTeacherExists(request.FirstName, request.LastName);

        Teacher teacher = new Teacher()
        {
            FirstName = request.FirstName,

            LastName = request.LastName,

            BirthDate = request.BirthDate,

            PhoneNumber = request.PhoneNumber,

            Email = request.Email,
            Img = SaveImage(request.Img),
        };

        await _dbContext.Teachers.AddAsync(teacher);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TeacherDto>(teacher);
    }

    private void FilterIfTeacherExists(string? FirstName, string? LastName)
    {
        Teacher? teacher = _dbContext.Teachers.FirstOrDefault(x => x.FirstName == FirstName && x.LastName == LastName);

        if (teacher is not null)
        {
            throw new AlreadyExistsException(" There is a  teacher with this full name. Teacher should be unique.  ");
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
