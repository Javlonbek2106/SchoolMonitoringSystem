using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolMonitoringSystem.Application.Common.Interfaces;

namespace SchoolMonitoringSystem.Application.Common.Services
{
    public class SaveImg : ISaveImg
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SaveImg(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string SaveImage(IFormFile imageFile)
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

            return uniqueFileName;
        }

    }
}