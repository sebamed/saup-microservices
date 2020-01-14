using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.DTO.Message
{
    public class CreateMessageRequestDTO {
        [Required]
        public string content { get; set; }
        [Required]
        public List<string> recipientsUUID { get; set; }
        public List<FileDTO> files { get; set; } 
    }
}
