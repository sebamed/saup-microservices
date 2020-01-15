using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class StudentResponseDTO: BaseDTO {
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string indexNumber { get; set; }
        public string departmentUUID { get; set; }
    }
}
