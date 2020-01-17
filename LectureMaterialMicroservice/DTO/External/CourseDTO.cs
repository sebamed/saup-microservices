using Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.DTO.External
{
    public class CourseDTO : BaseDTO {
        public string name { get; set; }

        public string description { get; set; }

        public string creationDate { get; set; }
    }
}
