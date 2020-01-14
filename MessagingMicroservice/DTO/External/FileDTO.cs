using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.DTO {
    public class FileDTO : BaseEntity {
        public string filePath { get; set; }
    }
}
