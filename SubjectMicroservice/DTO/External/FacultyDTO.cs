using Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectMicroservice.DTO.Department
{
    public class FacultyDTO : BaseDTO {
        public string name { get; set; }
        public string city { get; set; }
        public string phone { get; set; }
    }
}
