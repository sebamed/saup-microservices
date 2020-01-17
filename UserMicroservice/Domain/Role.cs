using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Domain {
    public class Role : BaseEntity {

        public int id { get; set; }

        public string name { get; set; }

    }
}
