using Commons.Domain;
using CourseMicroservice.DTO.Course;
using System.Collections.Generic;

namespace CourseMicroservice.Services {
    public interface ICourseStudentsService {
        //GET METHODS
        List<CourseStudentResponseDTO> GetAllStudentsOnCourse(string uuid);
        //PUT METHODS
        CourseStudentResponseDTO UpdateStudentOnCourse(string uuid,CourseStudentRequestDTO request);
        //POST METHODS
        CourseStudentResponseDTO CreateStudentOnCourse(string uuid, CourseStudentRequestDTO request);
        //DELETE METHODS
        CourseStudentResponseDTO DeleteStudentOnCourse(string courseUuid, string studentUuid);
    }
}
