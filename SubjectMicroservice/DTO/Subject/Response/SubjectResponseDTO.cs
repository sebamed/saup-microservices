using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.DTO;
using SubjectMicroservice.DTO.Department;
using SubjectMicroservice.DTO.External;

namespace SubjectMicroservice.DTO.Subject.Response {
    public class SubjectResponseDTO : BaseDTO {

        public string name { get; set; }

        public string description { get; set; }

        public DateTime creationDate { get; set; }

        public DepartmentDTO department { get; set; }

        public UserDTO creator { get; set; }
    }
}
