using MailService.DTO.Mail.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Services {
    public interface IMailService {

        void SendEmail(SendMailRequestDTO requestDTO);

    }
}
