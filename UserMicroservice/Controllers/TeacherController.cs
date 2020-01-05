using System.Collections.Generic;
using System.Net.Http;
using Commons.Domain;
using Commons.HttpClientRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using UserMicroservice.DTO.Teacher.Request;
using UserMicroservice.DTO.Teacher.Response;
using UserMicroservice.DTO.User;
using UserMicroservice.Localization;
using UserMicroservice.Services;

namespace UserMicroservice.Controllers {

    [Authorize]
    [Route(RouteConsts.ROUTE_TEACHER_BASE)]
    [ApiController]
    public class TeacherController : ControllerBase {

        private readonly HttpClientService _httpClientService;

        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService, HttpClientService httpClientService) {
            _teacherService = teacherService;
            this._httpClientService = httpClientService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<TeacherResponseDTO>> HandleGetAllTeachers() {
            var a = this._httpClientService.SendRequest<List<UserResponseDTO>>(HttpMethod.Get, "http://localhost:40001/api/users/", new UserPrincipal(HttpContext).token).Result;
            return Ok(this._teacherService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_TEACHER_GET_ONE_BY_UUID)]
        public ActionResult<TeacherResponseDTO> HandleGetOneTeacherByUuid(string uuid) {
            return Ok(this._teacherService.GetOneByUuid(uuid));
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<TeacherResponseDTO> HandleCreateAdmin(CreateTeacherRequestDTO requestDTO) {
            // todo: created()
            return Ok(this._teacherService.Create(requestDTO));
        }
    }
}