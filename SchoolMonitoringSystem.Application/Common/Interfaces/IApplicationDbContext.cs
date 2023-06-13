using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.Common;

public  interface IApplicationDbContext
{
    DbSet<T> Set<T>() where T : class;
    DbSet<Teacher> Teachers { get; }
    DbSet<Student> Students { get; }
    DbSet<Subject> Subjects { get; }
    DbSet<Grade> Grades { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
