using Commons.Consts;
using MessagingMicroservice.DTO;
using MessagingMicroservice.DTO.Message;
using MessagingMicroservice.Localization;
using MessagingtMicroservice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MessagingMicroservice.Controllers
{
    [Authorize]
    [Route(RouteConsts.ROUTE_MESSAGING_BASE)]
    [ApiController]
    public class MessagingController : ControllerBase
    {

        private readonly IMessagingService _messagingService;

        public MessagingController(IMessagingService messagingService) {
            _messagingService = messagingService;
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet]
        public ActionResult<List<MessageResponseDTO>> HandleGetAllMessages() => Ok(this._messagingService.GetAll());

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_MESSAGING_BY_RECIPIENT)]
        public ActionResult<List<MessageResponseDTO>> HandleGetAllMessagesByRecipients(string recipients)
        {
            return Ok(this._messagingService.GetMessagesByRecipents(recipients));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpPost]
        public ActionResult<MessageResponseDTO> HandleCreateMessage(CreateMessageRequestDTO requestDTO){
           return Ok(this._messagingService.Create(requestDTO));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpDelete]
        public ActionResult<MessageResponseDTO> HandleDeleteMessage(string uuid)
        {
            return Ok(this._messagingService.Delete(uuid));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpPut(RouteConsts.ROUTE_MESSAGING_UPDATE_FILE)]
        public ActionResult<string> HandleUpdateMessage(FileDTO request)
        {
            return Ok(this._messagingService.UpdateFileInMessage(request));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpPut(RouteConsts.ROUTE_MESSAGING_UPDATE_RECIPIENT)]
        public ActionResult<string> HandleUpdateMessage(UserDTO recipient)
        {
            return Ok(this._messagingService.UpdateRecipientInMessage(recipient));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpPut(RouteConsts.ROUTE_MESSAGING_UPDATE_SENDER)]
        public ActionResult<string> HandleUpdateMessageSender(UserDTO sender)
        {
            return Ok(this._messagingService.UpdateSenderInMessage(sender));
        }
    }
}