using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMicroservice.Domain.External
{
    public class Student: BaseEntity
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string indexNumber { get; set; }
    }
}
