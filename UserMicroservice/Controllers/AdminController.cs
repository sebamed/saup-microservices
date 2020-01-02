using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.DTO.Admin.Request;
using UserMicroservice.DTO.Admin.Response;
using UserMicroservice.Localization;
using UserMicroservice.Services;

namespace UserMicroservice.Controllers {

    [Authorize]
    [Route(RouteConsts.ROUTE_ADMIN_BASE)]
    [ApiController]
    public class AdminController : ControllerBase {

        private readonly IAdminService _adminService;

        public AdminController(IAdminService userService) {
            _adminService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<AdminResponseDTO>> HandleGetAllAdmins() {
            return Ok(this._adminService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_ADMIN_GET_ONE_BY_UUID)]
        public ActionResult<AdminResponseDTO> HandleGetOneAdminByUuid(string uuid) {
            return Ok(this._adminService.GetOneByUuid(uuid));
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<AdminResponseDTO> HandleCreateAdmin(CreateAdminRequestDTO requestDTO) {
            // todo: created()
            return Ok(this._adminService.Create(requestDTO));
        }

    }
}
