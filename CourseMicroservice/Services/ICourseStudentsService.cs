using Commons.Domain;
using CourseMicroservice.DTO.Course;
using System.Collections.Generic;

namespace CourseMicroservice.Services {
    public interface ICourseStudentsService {
        //GET METHODS
        List<CourseStudentMultipleResponseDTO> GetAllActiveStudentsOnCourse(string uuid);
        //POST METHODS
        CourseStudentResponseDTO CreateStudentOnCourse(string uuid, CreateCourseStudentRequestDTO request);
        //DELETE METHODS
        CourseStudentResponseDTO DeleteStudentOnCourse(string courseUuid, string studentUuid);
        //PUT METHODS
        CourseStudentResponseDTO UpdateStudentOnCourse(string uuid, UpdateCourseStudentRequestDTO request);
    }
}
