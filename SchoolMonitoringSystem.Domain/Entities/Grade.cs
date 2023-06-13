using System.ComponentModel.DataAnnotations.Schema;
using SchoolMonitoringSystem.Domain.Common.BaseEntities;

namespace SchoolMonitoringSystem.Domain.Entities
{
    public class Grade : BaseAuditableEntity
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }    
        public decimal Ball { get; set; }
    }
}
