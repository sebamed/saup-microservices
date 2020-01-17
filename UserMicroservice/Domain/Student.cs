using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Domain.Base;

namespace UserMicroservice.Domain {
    public class Student : BaseUser {

        public string departmentUuid { get; set; }

        public string indexNumber { get; set; }

    }
}
