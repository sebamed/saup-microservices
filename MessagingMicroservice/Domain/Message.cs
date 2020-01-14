using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.Domain {
    public class Message : BaseEntity {
        public int id { get; set; }
        public string content { get; set; }
        public DateTime dateTime { get; set; }
        public User sender { get; set; }
        public List<User> recipients { get; set; }
        public List<File> files { get; set; }
    }
}
