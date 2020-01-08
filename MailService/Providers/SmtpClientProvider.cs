using MailService.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailService.Providers {
    public class SmtpClientProvider {

        public SmtpClient smtpClient { get; }

        public IOptions<EmailSenderConfigurationModel> config { get; }

        public SmtpClientProvider(IOptions<EmailSenderConfigurationModel> config) {
            this.config = config;
            
            NetworkCredential credentials = new NetworkCredential(this.config.Value.username, this.config.Value.password);

            this.smtpClient = new SmtpClient() {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = credentials
            };
        }

    }
}
