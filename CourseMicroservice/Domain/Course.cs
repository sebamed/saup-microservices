using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseMicroservice.Domain {
    public class Course : BaseEntity {

        public int id { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public int maxStudents { get; set; }
        public int minStudents { get; set; }
        public DateTime creationDate { get; set; }

        public string subjectUUID { get; set; }

    }
}
