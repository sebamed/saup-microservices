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
    public class FacultyController : ControllerBase {

        private readonly IFacultyService _facultyService;

        public FacultyController(IFacultyService facultyService) {
            _facultyService = facultyService;
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_FACULTY_BASE)]
        public ActionResult<List<FacultyResponseDTO>> HandleGetAll() {
            return Ok(this._facultyService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_FACULTY_BY_UUID)]
        public ActionResult<FacultyResponseDTO> HandleGetOnebyUUID(string uuid) {
            return Ok(this._facultyService.GetOneByUuid(uuid));
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_FACULTY_BY_NAME)]
        public ActionResult<List<FacultyResponseDTO>> HandleGetByName(string name) {
            return Ok(this._facultyService.GetByName(name));
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_FACULTY_BY_CITY)]
        public ActionResult<List<FacultyResponseDTO>> HandleGetByCity(string city) {
            return Ok(this._facultyService.GetByCity(city));
        }

        [AllowAnonymous]
        [HttpPost(RouteConsts.ROUTE_FACULTY_BASE)]
        public ActionResult<FacultyResponseDTO> HandleCreateFaculty(CreateFacultyRequestDTO requestDTO) {
            return Ok(this._facultyService.Create(requestDTO));
        }

        [AllowAnonymous]
        [HttpPut(RouteConsts.ROUTE_FACULTY_BASE)]
        public ActionResult<FacultyResponseDTO> HandleUpdateFaculty(UpdateFacultyRequestDTO requestDTO){
            return Ok(this._facultyService.Update(requestDTO));
        }

        [AllowAnonymous]
        [HttpDelete(RouteConsts.ROUTE_FACULTY_BY_UUID)]
        public ActionResult<FacultyResponseDTO> HandleDeleteByUUID(string uuid) {
            return Ok(this._facultyService.Delete(uuid));
        }
    }
}