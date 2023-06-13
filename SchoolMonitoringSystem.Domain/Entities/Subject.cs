using System.ComponentModel.DataAnnotations.Schema;
using SchoolMonitoringSystem.Domain.Common.BaseEntities;

namespace SchoolMonitoringSystem.Domain.Entities
{
    public class Subject : BaseAuditableEntity
    {
        public string SubjectName { get; set; }
        public Guid TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
