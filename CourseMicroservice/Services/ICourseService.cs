using CourseMicroservice.DTO.Course;
using System.Collections.Generic;

namespace CourseMicroservice.Services {
    public interface ICourseService : ICrudService<CourseResponseDTO> {
        CourseResponseDTO Create(CreateCourseRequestDTO requestDTO);

        CourseResponseDTO Update(UpdateCourseRequestDTO requestDTO);

        CourseResponseDTO Delete(string uuid);

        List<CourseTeacherResponseDTO> GetCourseTeachers(string uuid);

        List<CourseStudentResponseDTO> GetCourseStudents(string uuid);

        List<CourseArchiveResponseDTO> GetCourseArchives(string uuid);
    }
}
