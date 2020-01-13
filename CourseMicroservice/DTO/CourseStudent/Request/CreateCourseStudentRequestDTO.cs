using Commons.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace CourseMicroservice.DTO.Course {
    public class CreateCourseStudentRequestDTO {
        [Required]
        public string studentUUID { get; set; }
        [Required]
        public DateTime beginDate { get; set; }
        [Required]
        public float currentPoints { get; set; }
        [Required]
        public int finalMark { get; set; }
    }
}
