using AutoMapper;
using SchoolMonitoringSystem.Application.UseCases;
using SchoolMonitoringSystem.Domain.Entities;

namespace SchoolMonitoringSystem.Application.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Teacher, TeacherDto>();
        CreateMap<Teacher, GetTeacherWithSubjectsDto>();
        CreateMap<Student, StudentDto>();
        CreateMap<Student, GetStudentsWithGrades>();
        CreateMap<Grade, GradeDto>();
        CreateMap<Subject, SubjectDto>();
    }
}
