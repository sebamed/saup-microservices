using Commons.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace CourseMicroservice.DTO.Course {
    public class UpdateCourseTeacherRequestDTO
    {

        [Required]
        public string courseUUID { get; set; }
        [Required]
        public string teacherUUID { get; set; }

    }
}
