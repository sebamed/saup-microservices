using SectionMicroservice.DTO.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.DTO.SectionArchive.Response
{
    public class SectionArchiveResponseDTO
    {
        public string sectionUUID { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public int visible { get; set; }

        public DateTime creationDate { get; set; }

        public CourseDTO course { get; set; }

        public UserDTO moderator { get; set; }

        public DateTime changeDate { get; set; }

        public int version { get; set; }


    }
}
