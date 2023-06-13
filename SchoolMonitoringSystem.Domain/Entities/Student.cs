using SchoolMonitoringSystem.Domain.Common.BaseEntities;

namespace SchoolMonitoringSystem.Domain.Entities
{
    public class Student : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int StudentRegNumber { get; set; }
        public virtual ICollection<Grade> Grades { get; set;}
    }
}
