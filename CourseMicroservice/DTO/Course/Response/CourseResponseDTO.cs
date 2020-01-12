using Commons.DTO;
using System;
using System.Collections.Generic;

namespace CourseMicroservice.DTO.Course {
    public class CourseResponseDTO : BaseDTO {

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public int maxtudents { get; set; }
        public int minStudents { get; set; }
        public DateTime creationDate { get; set; }
        public string subjectUUID { get; set; }
        public List<CourseTeacherResponseDTO> teachers { get; set; }
        public List<CourseStudentResponseDTO> students { get; set; }
        public SubjectResponseDTO subject { get; set; }

    }
}
