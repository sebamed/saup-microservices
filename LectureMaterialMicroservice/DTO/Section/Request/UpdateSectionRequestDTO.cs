using Commons.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.DTO.Section.Request
{
    public class UpdateSectionRequestDTO : BaseDTO {
        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public int visible { get; set; }

        [Required]
        public DateTime creationDate { get; set; }
    }
}
