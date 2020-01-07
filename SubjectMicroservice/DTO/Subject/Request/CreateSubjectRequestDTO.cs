using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectMicroservice.DTO.Subject.Request {
    public class CreateSubjectRequestDTO {
        [Required]
        public string uuid { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public DateTime creationDate { get; set; }

    }
}
