using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.DTO.Teacher.Request;
using UserMicroservice.DTO.Teacher.Response;
using UserMicroservice.Localization;
using UserMicroservice.Services;

namespace UserMicroservice.Controllers {

    [Authorize]
    [Route(RouteConsts.ROUTE_TEACHER_BASE)]
    [ApiController]
    public class TeacherController : ControllerBase {

        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService) {
            _teacherService = teacherService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<TeacherResponseDTO>> HandleGetAllTeachers() {
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