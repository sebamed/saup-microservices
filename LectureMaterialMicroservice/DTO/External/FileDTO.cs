using Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.DTO.External
{
    public class FileDTO : BaseDTO {
        public string filePath { get; set; }
    }
}
