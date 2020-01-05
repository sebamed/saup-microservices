using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentMicroservice.Domain {
    public class Department : BaseEntity {
        public int id { get; set; }
        public string name { get; set; }
        public Faculty faculty { get; set; }
    }
}
