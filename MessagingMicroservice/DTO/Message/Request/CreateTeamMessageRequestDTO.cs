using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.DTO.Message
{
    public class CreateTeamMessageRequestDTO : CreateMessageRequestDTO
    {
        [Required]
        public string teamUUID { get; set; }
    }
}
