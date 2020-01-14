using AutoMapper;
using CourseMicroservice.Domain;
using CourseMicroservice.DTO.Course;

namespace CourseMicroservice.Mappers {
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //source, destination --destination mora imati iste nazive obelezja(moze i manje)
            CreateMap<Course, CourseResponseDTO>();
            CreateMap<UpdateCourseRequestDTO, Course>();
            CreateMap<CreateCourseRequestDTO, Course>();
            CreateMap<Course, CourseMultipleResponseDTO>();

            CreateMap<CourseTeacher, CourseTeacherResponseDTO>();
            CreateMap<CourseTeacher, CourseTeacherResponseDTO>();
            CreateMap<CreateCourseTeacherRequestDTO, CourseTeacher>();
            CreateMap<CourseTeacher, CourseTeacherMultipleResponseDTO>();

            CreateMap<CourseStudent, CreateCourseStudentRequestDTO>();
            CreateMap<CourseStudent, CourseStudentResponseDTO>();
            CreateMap<CreateCourseStudentRequestDTO, CourseStudent>();
            CreateMap<CourseStudent, CourseStudentMultipleResponseDTO>();

            CreateMap<CourseArchive, CourseArchiveResponseDTO>();
            CreateMap<CreateCourseArchiveRequestDTO, CourseArchive>();
            CreateMap<CourseArchive, CreateCourseArchiveRequestDTO>();

            CreateMap<Teacher, TeacherResponseDTO>();
            CreateMap<Student, StudentResponseDTO>();
            CreateMap<Subject, SubjectResponseDTO>();
        }
    }
}
