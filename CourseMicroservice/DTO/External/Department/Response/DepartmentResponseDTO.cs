using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class DepartmentResponseDTO: BaseDTO {
        public string name { get; set; }
        public string uuid { get; set; }

    }
}
