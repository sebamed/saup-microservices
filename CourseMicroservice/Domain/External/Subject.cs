using Commons.Domain;
using Commons.DTO;
using System;

namespace CourseMicroservice.Domain {
    public class Subject: BaseEntity {
        public string name { get; set; }
        public string description { get; set;}
        public DateTime creationDate { get; set; }
    }
}
