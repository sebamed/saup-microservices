using Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentMicroservice.DTO.User {
    public class DepartmentResponseDTO: BaseDTO {
        public string name { get; set; }

        public FacultyResponseDTO faculty { get; set; }
    }
}
