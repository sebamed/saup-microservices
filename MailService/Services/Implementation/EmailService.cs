using System.Net.Mail;
using MailService.Configuration;
using MailService.DTO.Mail.Request;
using MailService.Providers;
using Microsoft.Extensions.Options;

namespace MailService.Services.Implementation {
    public class EmailService : IMailService {


        private readonly SmtpClientProvider _smtpClientProvider;

        public EmailService(SmtpClientProvider smtpClientProvider) {
            this._smtpClientProvider = smtpClientProvider;
        }

        public void SendEmail(SendMailRequestDTO requestDTO) {
            MailMessage message = new MailMessage() {
                From = new MailAddress(this._smtpClientProvider.config.Value.from),
                Subject = requestDTO.title,
                Body = requestDTO.content
            };

            requestDTO.recipients.ForEach(r => message.To.Add(r));

            this._smtpClientProvider.smtpClient.Send(message);
        }

    }
}
