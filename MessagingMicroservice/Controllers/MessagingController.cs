using Commons.Consts;
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

        [AllowAnonymous]//todo [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet]
        public ActionResult<List<MessageResponseDTO>> HandleGetAllMessages() => Ok(this._messagingService.GetAll());

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpPost]
        public ActionResult<MessageResponseDTO> HandleCreateMessage(CreateMessageRequestDTO requestDTO){
           return Ok(this._messagingService.Create(requestDTO));
        }

        [AllowAnonymous]//todo [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpPut]
        public ActionResult<MessageResponseDTO> HandleUpdateMessage(UpdateMessageRequestDTO requestDTO)
        {
            return Ok(this._messagingService.Update(requestDTO));
        }

        [AllowAnonymous]//todo [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpDelete]
        public ActionResult<MessageResponseDTO> HandleDeleteMessage(string uuid)
        {
            return Ok(this._messagingService.Delete(uuid));
        }
    }
}