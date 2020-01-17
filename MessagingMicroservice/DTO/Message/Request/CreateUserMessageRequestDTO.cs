using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.DTO.Message
{
    public class CreateUserMessageRequestDTO : CreateMessageRequestDTO
    {
        [Required]
        public string recipientUUID { get; set; }
    }
}
