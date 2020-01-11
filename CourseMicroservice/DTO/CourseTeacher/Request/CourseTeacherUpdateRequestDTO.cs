using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class CourseTeacherUpdateRequest {
        public string teacherUUID { get; set; }
        public bool activeTeacher { get; set; }

    }
}
