using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DepartmentMicroservice.DTO.User;
using DepartmentMicroservice.Localization;
using DepartmentMicroservice.Services;
using DepartmentMicroservice.DTO.User.Request;
using Commons.Consts;

namespace DepartmentMicroservice.Controllers {
    [Authorize]
    [Route(RouteConsts.ROUTE_DEPARTMENT_BASE)]
    [ApiController]
    public class DepartmentController : ControllerBase {

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService) {
			_departmentService = departmentService;
        }

        [Authorize(Roles = RoleConsts.ROLE_STUDENT)]
        [HttpGet(RouteConsts.ROUTE_DEPARTMENT_BASE)]
        public ActionResult<List<DepartmentResponseDTO>> HandleGetAll() {
            return Ok(this._departmentService.GetAll());
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_DEPARTMENT_BY_UUID)]
        public ActionResult<DepartmentResponseDTO> HandleGetOnebyUUID(string uuid) {
            return Ok(this._departmentService.GetOneByUuid(uuid));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_DEPARTMENT_BY_NAME)]
        public ActionResult<List<DepartmentResponseDTO>> HandleGetByName(string name) {
            return Ok(this._departmentService.GetByName(name));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_DEPARTMENT_BY_FACULTY_NAME)]
        public ActionResult<List<DepartmentResponseDTO>> HandleGetByFacultyName(string facultyName)
        {
            return Ok(this._departmentService.GetByFacultyName(facultyName));
        }


        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        [HttpPost(RouteConsts.ROUTE_DEPARTMENT_BASE)]
        public ActionResult<DepartmentResponseDTO> HandleCreateDepartment(CreateDepartmentRequestDTO requestDTO) {
            return Ok(this._departmentService.Create(requestDTO));
        }

        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        [HttpPut(RouteConsts.ROUTE_DEPARTMENT_BASE)]
        public ActionResult<DepartmentResponseDTO> HandleUpdateDepartment(UpdateDepartmentRequestDTO requestDTO){
            return Ok(this._departmentService.Update(requestDTO));
        }

        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        [HttpDelete(RouteConsts.ROUTE_DEPARTMENT_BY_UUID)]
        public ActionResult<DepartmentResponseDTO> HandleDeleteByUUID(string uuid) {
            return Ok(this._departmentService.Delete(uuid));
        }
    }
}