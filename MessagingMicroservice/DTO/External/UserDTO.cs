using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.DTO {
    public class UserDTO : BaseEntity {
        public string name { get; set; }
        public string surname { get; set; }
    }
}
