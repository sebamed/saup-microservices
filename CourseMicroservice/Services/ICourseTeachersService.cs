using Commons.Domain;
using CourseMicroservice.DTO.Course;
using System.Collections.Generic;

namespace CourseMicroservice.Services {
    public interface ICourseTeacherService {
        //GET METHODS
        List<CourseTeacherResponseDTO> GetAllTeachersOnCourse(string uuid);
        //PUT METHODS
        CourseTeacherResponseDTO UpdateTeacherOnCourse(string uuid, CourseTeacherUpdateRequest request);
        //DELETE METHODS
        CourseTeacherResponseDTO DeleteTeacherOnCourse(string courseUUID, string teacherUUID);
        //POST METHODS
        CourseTeacherResponseDTO CreateTeacherOnCourse(string courseUUID, CourseTeacherUpdateRequest request);
    }
}
