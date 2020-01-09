using CourseMicroservice.DTO.Course;

namespace CourseMicroservice.Services {
    public interface ICourseService : ICrudService<CourseResponseDTO> {
        CourseResponseDTO Create(CreateCourseRequestDTO requestDTO);

        CourseResponseDTO Update(UpdateCourseRequestDTO requestDTO);

        CourseResponseDTO Delete(string uuid);
    }
}
