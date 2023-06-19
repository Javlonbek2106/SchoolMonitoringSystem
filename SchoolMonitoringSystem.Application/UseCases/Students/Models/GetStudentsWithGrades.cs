using Microsoft.AspNetCore.Http;

namespace SchoolMonitoringSystem.Application.UseCases;
public class GetStudentsWithGrades : StudentDto
{

    public ICollection<GradeDto>? Grades { get; set; }
}
