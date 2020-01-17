using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Domain.Base {
    public class BaseUser : BaseEntity {

        public int id { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

    }
}
