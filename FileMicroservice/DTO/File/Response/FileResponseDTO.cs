using Commons.DTO;
using FileMicroservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileMicroservice.DTO {
    public class FileResponseDTO: BaseDTO {
        public string filePath { get; set; }
    }
}
