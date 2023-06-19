namespace SchoolMonitoringSystem.Application.UseCases;

public  class GetTeacherWithSubjectsDto : TeacherDto
    {
        public List<SubjectDto> Subjects { get; set; }
    }

