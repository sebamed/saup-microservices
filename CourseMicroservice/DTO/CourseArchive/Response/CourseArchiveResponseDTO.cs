using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class CourseArchiveResponseDTO {
        public string courseUUID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public int maxStudents { get; set; }
        public int minStudents { get; set; }
        public DateTime creationDate { get; set; }
        public string subjectUUID { get; set; }
        public string moderatorUUID { get; set; }
        public DateTime changeDate { get; set; }
        public int version { get; set; }

    }
}
