using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.DTO;


namespace SubjectMicroservice.DTO.SubjectArchive.Response {
    public class MultipleSubjectArchiveResponseDTO {

        public string subjectUUID { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public DateTime creationDate { get; set; }

        public string departmentUUID { get; set; }

        public string creatorUUID { get; set; }

        public string moderatorUUID { get; set; }

        public DateTime changeDate { get; set; }

        public int version { get; set; }
    }
}
