using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.Domain
{
    public class SectionArchive : BaseEntity {
        public string sectionUUID { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public int visible { get; set; }

        public DateTime creationDate { get; set; }

        //public Course course { get; set; }

        public int moderatorID { get; set; }

        public DateTime changeDate { get; set; }

        public int version { get; set; }
    }
}
