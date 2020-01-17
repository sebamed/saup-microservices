using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class SubjectResponseDTO: BaseDTO {
        public string name { get; set; }
        public string description { get; set;}
        public DateTime creationDate { get; set; }
        public DepartmentResponseDTO department { get; set; }
    }
}
