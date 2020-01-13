using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class CourseTeacherMultipleResponseDTO {
        public string teacherUUID { get; set; }
        public bool activeTeacher { get; set; }
    }
}
