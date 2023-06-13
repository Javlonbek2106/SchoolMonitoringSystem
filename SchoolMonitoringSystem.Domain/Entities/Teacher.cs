using SchoolMonitoringSystem.Domain.Common.BaseEntities;

namespace SchoolMonitoringSystem.Domain.Entities
{
    public class Teacher : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
