using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.Domain.External
{
    public class Course : BaseEntity {

        public string name { get; set; }

        public string description { get; set; }

        public string creationDate { get; set; }
    }
}
