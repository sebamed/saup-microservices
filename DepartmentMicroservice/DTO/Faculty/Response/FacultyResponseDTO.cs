using Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentMicroservice.DTO.User {
    public class FacultyResponseDTO: BaseDTO {
        public string name { get; set; }
        public string city { get; set; }
        public string phone { get; set; }
    }
}
