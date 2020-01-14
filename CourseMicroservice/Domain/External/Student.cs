using Commons.Domain;
using Commons.DTO;
using System;

namespace CourseMicroservice.Domain {
    public class Student: BaseEntity {
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string indexNumber { get; set; }
    }
}
