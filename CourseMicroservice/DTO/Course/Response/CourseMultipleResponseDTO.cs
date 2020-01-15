using Commons.DTO;
using System;
using System.Collections.Generic;

namespace CourseMicroservice.DTO.Course {
    public class CourseMultipleResponseDTO : BaseDTO {
        public string name { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public int maxStudents { get; set; }
        public int minStudents { get; set; }
        public DateTime creationDate { get; set; }
        public string subjectUUID { get; set; }
    }
}
