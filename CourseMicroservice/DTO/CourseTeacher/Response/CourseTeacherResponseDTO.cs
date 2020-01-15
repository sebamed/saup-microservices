using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class CourseTeacherResponseDTO {
        public CourseResponseDTO course { get; set; }
        public TeacherResponseDTO teacher { get; set; }
        public bool activeTeacher { get; set; }
    }
}
