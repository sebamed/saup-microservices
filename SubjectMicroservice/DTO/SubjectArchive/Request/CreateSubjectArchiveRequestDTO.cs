﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectMicroservice.DTO.SubjectArchive.Request {
    public class CreateSubjectArchiveRequestDTO {
        [Required]
        public string subjectUUID { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public DateTime creationDate { get; set; }
        
        [Required]
        public string departmentUUID { get; set; }
        
        [Required]
        public string creatorUUID { get; set; }
        
        [Required]
        public string moderatorUUID { get; set; }
        
        [Required]
        public DateTime changeDate { get; set; }
    }
}
