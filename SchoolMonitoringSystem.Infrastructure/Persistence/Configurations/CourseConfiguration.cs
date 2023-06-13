//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using StudentPaymentSystem.Domain.Entities;

//namespace StudentPaymentSystem.Infrastructure.Persistence.Configurations;

//public class CourseConfiguration : IEntityTypeConfiguration<Course>
//{
//    public void Configure(EntityTypeBuilder<Course> builder)
//    {
//       // builder.Navigation(x => x.Students).AutoInclude();
//        builder.HasIndex(x => x.Name).IsUnique();
//    }
//}
