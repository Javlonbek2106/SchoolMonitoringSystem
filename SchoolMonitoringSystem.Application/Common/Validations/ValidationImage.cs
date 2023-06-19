using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SchoolMonitoringSystem.Application.Common.Validations
{
    public class ValidateImage : ValidationAttribute
    {
        string[]? extensions = null;
        int maxSize;
        public ValidateImage(int maxSize, params string[]? extensions)
        {
            this.maxSize = maxSize;
            this.extensions = extensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > maxSize)
                {
                    return new ValidationResult($"File size should not excess {maxSize / (1024 * 1024)}Mb");
                }
                if (extensions is not null)
                    if (!extensions.Contains(Path.GetExtension(file.FileName)))
                    {
                        return new ValidationResult($" You can download files only with extensions {string.Join(',', extensions)}");
                    }
            }

            return ValidationResult.Success;
        }


    }
}
