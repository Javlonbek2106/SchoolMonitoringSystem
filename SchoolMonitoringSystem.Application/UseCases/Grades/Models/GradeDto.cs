namespace SchoolMonitoringSystem.Application.UseCases;

public class GradeDto
{
    public Guid Id { get; set; }
    public Guid SubjectId { get; set; }

    public Guid StudentId { get; set; }

    public decimal Ball { get; set; }
}
