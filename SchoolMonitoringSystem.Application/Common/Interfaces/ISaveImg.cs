using Microsoft.AspNetCore.Http;

namespace SchoolMonitoringSystem.Application.Common.Interfaces
{
    public interface ISaveImg
    {
        string SaveImage(IFormFile newFile);
    }
}