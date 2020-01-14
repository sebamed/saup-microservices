using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class TeacherResponseDTO: BaseDTO {
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }

    }
}
