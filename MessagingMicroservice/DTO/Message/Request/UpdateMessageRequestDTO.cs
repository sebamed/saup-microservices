using Commons.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.DTO.Message {
    public class UpdateMessageRequestDTO: BaseDTO {//todo obrisati sav update
        [Required(ErrorMessage = "Content of message is required")]
        public string content { get; set; }
        [Required]
        public DateTime dateTime { get; set; }
        public string senderUUID { get; set; }
    }
}
