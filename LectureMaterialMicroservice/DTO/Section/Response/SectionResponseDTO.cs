using Commons.DTO;
using SectionMicroservice.DTO.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LectureMaterialMicroservice.DTO.User {
    public class SectionResponseDTO : BaseDTO{

        public string name { get; set; }

        public string description { get; set; }

        public int visible { get; set; }

        public DateTime creationDate { get; set; }

        public CourseDTO course { get; set; }
    }
}
