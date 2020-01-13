using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.Domain {
    public class File : BaseEntity {
        public string filePath { get; set; }
    }
}
