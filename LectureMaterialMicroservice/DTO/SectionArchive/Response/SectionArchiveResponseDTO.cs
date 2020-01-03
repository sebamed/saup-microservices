using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.DTO.SectionArchive.Response
{
    public class SectionArchiveResponseDTO
    {
        public string name { get; set; }

        public string description { get; set; }

        public int visible { get; set; }

        public DateTime creationDate { get; set; }

        public int moderatorID { get; set; }

        public DateTime changeDate { get; set; }

        public int version { get; set; }
    }
}
