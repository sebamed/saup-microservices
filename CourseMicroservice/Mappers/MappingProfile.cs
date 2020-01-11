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
            CreateMap<CourseTeacher, CourseTeacherResponseDTO>();
            CreateMap<CourseStudent, CourseStudentResponseDTO>();
            CreateMap<CourseArchive, CourseArchiveResponseDTO>();
            CreateMap<CourseTeacher, CourseTeacherResponseDTO>();
            CreateMap<CourseTeacherUpdateRequest, CourseTeacher>();
        }
    }
}
