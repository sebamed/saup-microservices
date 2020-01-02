﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LectureMaterialMicroservice.Consts;
using LectureMaterialMicroservice.DTO.User;
using LectureMaterialMicroservice.Localization;
using LectureMaterialMicroservice.Services;
using Commons.Consts;

namespace LectureMaterialMicroservice.Controllers
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

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_USER_GET_ONE_BY_UUID)]
        public ActionResult<UserResponseDTO> HandleGetOneUserByUuid(string uuid) {
            return Ok(this._userService.GetOneByUuid(uuid));
        }

    }
}