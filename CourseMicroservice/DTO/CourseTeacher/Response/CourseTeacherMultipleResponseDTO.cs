using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class CourseTeacherMultipleResponseDTO {
        public string courseUUID { get; set; }
        public string teacherUUID { get; set; }
        public bool activeTeacher { get; set; }
    }
}
