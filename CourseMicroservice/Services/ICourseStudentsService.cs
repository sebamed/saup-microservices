using Commons.Domain;
using CourseMicroservice.DTO.Course;
using System.Collections.Generic;

namespace CourseMicroservice.Services {
    public interface ICourseStudentsService {
        //GET METHODS
        List<CourseStudentMultipleResponseDTO> GetAllActiveStudentsOnCourse(string uuid);
        List<CourseStudentMultipleResponseDTO> GetAllCoursesByStudentUuid(string studentUuid);
        List<CourseStudentMultipleResponseDTO> GetStudentSubjectHistory(string studentuuid, string subjectuuid);
        //POST METHODS
        CourseStudentResponseDTO CreateStudentOnCourse(CreateCourseStudentRequestDTO request);
        //DELETE METHODS
        CourseStudentResponseDTO DeleteStudentOnCourse(string courseUuid, string studentUuid);
        //PUT METHODS
        CourseStudentResponseDTO UpdateStudentOnCourse(UpdateCourseStudentRequestDTO request);
    }
}
