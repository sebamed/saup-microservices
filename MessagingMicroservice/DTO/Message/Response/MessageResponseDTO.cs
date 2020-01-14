using Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.DTO.Message{
    public class MessageResponseDTO: BaseDTO {
        public string content { get; set; }
        public DateTime dateTime { get; set; }
        public UserDTO sender { get; set; }
        public List<UserDTO> recipients { get; set; }
        public List<FileDTO> files { get; set; }
    }
}
