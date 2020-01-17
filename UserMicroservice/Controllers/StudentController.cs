using System.Collections.Generic;
using Commons.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.DTO.Student.Request;
using UserMicroservice.DTO.Student.Response;
using UserMicroservice.Localization;
using UserMicroservice.Services;

namespace UserMicroservice.Controllers {
    [Authorize]
    [Route(RouteConsts.ROUTE_STUDENT_BASE)]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService) {
            _studentService = studentService;
        }

        [Authorize(Roles = RoleConsts.ROLE_STUDENT_ONLY)]
        [HttpGet]
        public ActionResult<List<StudentResponseDTO>> HandleGetAllStudents() {
            return Ok(this._studentService.GetAll());
        }

        [Authorize(Roles = RoleConsts.ROLE_STUDENT_ONLY)]
        [HttpGet(RouteConsts.ROUTE_STUDENT_GET_ONE_BY_UUID)]
        public ActionResult<StudentResponseDTO> HandleGetOneStudentByUuid(string uuid) {
            return Ok(this._studentService.GetOneByUuid(uuid));
        }

        [Authorize(Roles = RoleConsts.ROLE_STUDENT_ONLY)]
        [HttpPost]
        public ActionResult<StudentResponseDTO> HandleCreateStudent(CreateStudentRequestDTO requestDTO) {
            return Ok(this._studentService.Create(requestDTO));
        }

        [Authorize(Roles = RoleConsts.ROLE_STUDENT_ONLY)]
        [HttpGet(RouteConsts.ROUTE_USER_GET_ONE_BY_UUID_FROM_DEPARTMENT)]
        public ActionResult<StudentWithDepartmantResponseDTO> HandleGetStudentInfoFromDepartmentByUuid(string uuid) {
            return Ok(this._studentService.GetStudentInfoFromDepartmentByUuid(uuid));
        }
    }
}