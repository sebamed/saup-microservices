using AutoMapper;
using CourseMicroservice.Domain;
using CourseMicroservice.DTO.Course;

namespace CourseMicroservice.Mappers {
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseResponseDTO>();
            CreateMap<UpdateCourseRequestDTO, Course>();
        }
    }
}
