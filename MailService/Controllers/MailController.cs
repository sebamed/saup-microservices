using Microsoft.AspNetCore.Mvc;
using MailService.Localization;
using MailService.Services;
using MailService.DTO.Mail.Request;

namespace MailService.Controllers {

    [Route(RouteConsts.ROUTE_MAIL_BASE)]
    [ApiController]
    public class MailController : ControllerBase
    {

        public readonly IMailService _mailService;

        public MailController(IMailService mailService) {
            this._mailService = mailService;
        }

        [HttpPost(RouteConsts.ROUTE_MAIL_SEND)]
        public ActionResult HandleSendEmail(SendMailRequestDTO requestDTO) {
            this._mailService.SendEmail(requestDTO);
            return NoContent();
        }

    }
}