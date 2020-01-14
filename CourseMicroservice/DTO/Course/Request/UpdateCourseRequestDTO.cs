using Commons.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace CourseMicroservice.DTO.Course {
    public class UpdateCourseRequestDTO : BaseDTO {
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public bool active { get; set; }
        [Required]
        public int maxtudents { get; set; }
        [Required]
        public int minStudents { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        [Required]
        public string subjectUUID { get; set; }
    }
}
