//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using StudentPaymentSystem.Domein.Entities;

//namespace StudentPaymentSystem.Infrustructure.Persistence.Configurations;

//public class StudentConfiguration : IEntityTypeConfiguration<Student>
//{
//    public void Configure(EntityTypeBuilder<Student> builder)
//    {
//      //  builder.Navigation(x => x.Courses).AutoInclude();
//        builder.Navigation(x=>x.Payments).AutoInclude();
//        builder.HasIndex(x => x.PhoneNumber).IsUnique();
//    }
//}
