using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.DTO.SectionArchive.Request
{
    public class CreateSectionArchiveRequestDTO {

        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public int visible { get; set; }

        [Required]
        public DateTime creationDate { get; set; }

        [Required]
        public int moderatorID { get; set; }

        [Required]
        public DateTime changeDate { get; set; }

        [Required]
        public int version { get; set; }
    }
}

