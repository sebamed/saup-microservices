using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.DTO;


namespace SubjectMicroservice.DTO.Subject.Response {
    public class SubjectResponseDTO : BaseDTO{

        public string uuid { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public DateTime creationDate { get; set; }


    }
}
