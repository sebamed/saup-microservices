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

            CreateMap<CourseTeacher, CourseTeacherResponseDTO>();
            CreateMap<CourseTeacher, CourseTeacherResponseDTO>();
            CreateMap<CourseTeacherRequestDTO, CourseTeacher>();

            CreateMap<CourseStudent, CourseStudentRequestDTO>();
            CreateMap<CourseStudent, CourseStudentResponseDTO>();
            CreateMap<CourseStudentRequestDTO, CourseStudent>();

            CreateMap<CourseArchive, CourseArchiveResponseDTO>();
        }
    }
}
