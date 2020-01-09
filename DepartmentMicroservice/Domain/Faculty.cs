using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentMicroservice.Domain {
    public class Faculty : BaseEntity {
        public int id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string phone { get; set; }
    }
}
