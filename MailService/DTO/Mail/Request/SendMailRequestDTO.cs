using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.DTO.Mail.Request {
    public class SendMailRequestDTO {

        public string title { get; set; }

        public string content { get; set; }

        public List<string> recipients { get; set; }

    }
}
