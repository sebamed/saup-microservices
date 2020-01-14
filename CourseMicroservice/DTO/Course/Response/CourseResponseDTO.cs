using Commons.DTO;
using System;
using System.Collections.Generic;

namespace CourseMicroservice.DTO.Course {
    public class CourseResponseDTO : BaseDTO {
        public string name { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public int maxStudents { get; set; }
        public int minStudents { get; set; }
        public DateTime creationDate { get; set; }
        public SubjectResponseDTO subject { get; set; }
    }
}
