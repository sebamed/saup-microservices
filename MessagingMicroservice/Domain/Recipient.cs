using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.Domain {
    public class Recipient : BaseEntity {
        public string messageUUID { get; set; }
        public User recipient { get; set; }
    }
}
