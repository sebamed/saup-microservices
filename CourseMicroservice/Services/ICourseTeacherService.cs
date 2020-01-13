using Commons.Domain;
using CourseMicroservice.DTO.Course;
using System.Collections.Generic;

namespace CourseMicroservice.Services {
    public interface ICourseTeacherService {
        //GET METHODS
        List<CourseTeacherMultipleResponseDTO> GetAllActiveTeachersOnCourse(string uuid);
        //DELETE METHODS
        CourseTeacherResponseDTO DeleteTeacherOnCourse(string courseUUID, string teacherUUID);
        //POST METHODS
        CourseTeacherResponseDTO CreateTeacherOnCourse(string courseUUID, CreateCourseTeacherRequestDTO request);
    }
}
