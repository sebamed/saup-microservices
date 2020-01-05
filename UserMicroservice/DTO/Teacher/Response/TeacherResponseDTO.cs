using Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.DTO.Teacher.Response {
    public class TeacherResponseDTO : BaseDTO {

        public string name { get; set; }

        public string surname { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

    }
}
