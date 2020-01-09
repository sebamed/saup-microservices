using Commons.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace CourseMicroservice.DTO.Course {
    public class UpdateCourseRequestDTO : BaseDTO {
        public string name { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public int maxtudents { get; set; }
        public int minStudents { get; set; }
        public DateTime creationDate { get; set; }
    }
}
