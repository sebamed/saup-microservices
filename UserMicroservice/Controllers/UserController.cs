using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Consts;
using UserMicroservice.DTO.User;
using UserMicroservice.Localization;
using UserMicroservice.Services;
using Commons.Consts;
using UserMicroservice.DTO.User.Request;

namespace UserMicroservice.Controllers
{
    [Authorize]
    [Route(RouteConsts.ROUTE_USER_BASE)]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet]
        public ActionResult<List<UserResponseDTO>> HandleGetAllUsers() {
            return Ok(this._userService.GetAll());
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpPost(RouteConsts.ROUTE_USER_CHANGE_PASSWORD)]
        public ActionResult<UserResponseDTO> HandleChangePassword(ChangePasswordRequestDTO requestDTO) {
            return Ok(this._userService.ChangePassword(requestDTO));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_USER_GET_ONE_BY_UUID)]
        public ActionResult<UserResponseDTO> HandleGetOneUserByUuid(string uuid) {
            return Ok(this._userService.GetOneByUuid(uuid));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpPut]
        public ActionResult<UserResponseDTO> HandleUpdateUser(UpdateUserRequestDTO requestDTO) {
            return Ok(this._userService.Update(requestDTO));
        }

    }
}