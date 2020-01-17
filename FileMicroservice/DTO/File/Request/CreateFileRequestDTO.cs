using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileMicroservice.DTO.File {
    public class CreateFileRequestDTO {
        [Required]
        public string fileData { get; set; }

        [Required]
        public string fileExtension { get; set; }
    }
}
