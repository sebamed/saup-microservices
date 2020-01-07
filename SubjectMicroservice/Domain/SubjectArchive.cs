using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectMicroservice.Domain
{
	public class SubjectArchive : BaseEntity{

        public string subjectUUID { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public DateTime creationDate { get; set; }

        public string departmentUUID { get; set; }

        public string creatorUUID { get; set; }

        public string moderatorUUID { get; set; }

        public int version { get; set; }

        public DateTime changeDate { get; set; }

    }
}
