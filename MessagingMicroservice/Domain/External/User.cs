using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.Domain {
    public class User : BaseEntity {
        public string name { get; set; }
        public string surname { get; set; }
    }
}
