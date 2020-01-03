using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DepartmentMicroservice.DTO.User;
using DepartmentMicroservice.Localization;
using DepartmentMicroservice.Services;
using DepartmentMicroservice.DTO.User.Request;

namespace DepartmentMicroservice.Controllers {
    [Authorize]
    [Route(RouteConsts.ROUTE_DEPARTMENT_BASE)]
    [ApiController]
    public class DepartmentController : ControllerBase {

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService) {
			_departmentService = departmentService;
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_DEPARTMENT_BASE)]
        public ActionResult<List<DepartmentResponseDTO>> HandleGetAll() {
            return Ok(this._departmentService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_DEPARTMENT_BY_UUID)]
        public ActionResult<DepartmentResponseDTO> HandleGetOnebyUUID(string uuid) {
            return Ok(this._departmentService.GetOneByUuid(uuid));
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_DEPARTMENT_BY_NAME)]
        public ActionResult<List<DepartmentResponseDTO>> HandleGetByName(string name) {
            return Ok(this._departmentService.GetByName(name));
        }


        [AllowAnonymous]
        [HttpPost(RouteConsts.ROUTE_DEPARTMENT_BASE)]
        public ActionResult<DepartmentResponseDTO> HandleCreateDepartment(CreateDepartmentRequestDTO requestDTO) {
            return Ok(this._departmentService.Create(requestDTO));
        }

        [AllowAnonymous]
        [HttpPut(RouteConsts.ROUTE_DEPARTMENT_BASE)]
        public ActionResult<DepartmentResponseDTO> HandleUpdateDepartment(UpdateDepartmentRequestDTO requestDTO){
            return Ok(this._departmentService.Update(requestDTO));
        }

        [AllowAnonymous]
        [HttpDelete(RouteConsts.ROUTE_DEPARTMENT_BY_UUID)]
        public ActionResult<DepartmentResponseDTO> HandleDeleteByUUID(string uuid) {
            return Ok(this._departmentService.Delete(uuid));
        }
    }
}