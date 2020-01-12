using Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LectureMaterialMicroservice.DTO.User {
    public class MultipleSectionResponseDTO : BaseDTO{

        public string name { get; set; }

        public string description { get; set; }

        public int visible { get; set; }

        public DateTime creationDate { get; set; }

        public string courseUUID { get; set; }
    }
}
