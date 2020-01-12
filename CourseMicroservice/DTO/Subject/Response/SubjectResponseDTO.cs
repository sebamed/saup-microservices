using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class SubjectResponseDTO {
        public string name { get; set; }
        public string uuid { get; set; }
        public string description { get; set;}
        public DateTime creationDate { get; set; }
    }
}
