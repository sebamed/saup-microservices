using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.Domain {
    public class FileMessage : BaseEntity {
        public string messageUUID { get; set; }//todo Message object
        public File file { get; set; }
    }
}
