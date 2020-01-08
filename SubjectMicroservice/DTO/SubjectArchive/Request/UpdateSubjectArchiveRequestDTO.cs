using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Commons.DTO;

namespace SubjectMicroservice.DTO.SubjectArchive.Request {
    public class UpdateSubjectArchiveRequestDTO {
        
        [Required]
        public string subjectUUID { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        public DateTime changeDate { get; set; }

        public int version { get; set; }
    }
}
