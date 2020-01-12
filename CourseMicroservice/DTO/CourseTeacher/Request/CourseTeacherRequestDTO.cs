using Commons.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace CourseMicroservice.DTO.Course {
    public class CourseTeacherRequestDTO {
        [Required]
        public string teacherUUID { get; set; }
        [Required]
        public bool activeTeacher { get; set; }

    }
}
