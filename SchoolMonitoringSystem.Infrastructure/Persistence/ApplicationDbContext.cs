using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext/*<IdentityUser>*/, IApplicationDbContext
    {

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }


        private readonly AuditableEntitySaveChangesInterceptor _interceptor;

        private readonly DbContextOptions<ApplicationDbContext>? options;
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>? options,
            AuditableEntitySaveChangesInterceptor interceptor) : base(options)
        {
            _interceptor=interceptor;
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.Name).Property(typeof(DateTimeOffset), "CreatedDate")
                    .HasColumnType("timestamptz");

                modelBuilder.Entity(entity.Name).Property(typeof(DateTimeOffset), "UpdatedDate")
                    .HasColumnType("timestamptz");
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_interceptor);
        }

      
    }

}

