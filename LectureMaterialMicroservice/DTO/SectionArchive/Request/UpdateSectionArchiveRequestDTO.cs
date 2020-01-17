using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.DTO.SectionArchive.Request
{
    public class UpdateSectionArchiveRequestDTO {
        [Required]
        public string sectionUUID { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public int visible { get; set; }

        [Required]
        public DateTime creationDate { get; set; }

        [Required]
        public string courseUUID { get; set; }

        [Required]
        public string moderatorUUID { get; set; }

        [Required]
        public DateTime changeDate { get; set; }
    }
}
