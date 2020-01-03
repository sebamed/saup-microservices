using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DepartmentMicroservice.Consts;
using DepartmentMicroservice.DTO.User;
using DepartmentMicroservice.Localization;
using DepartmentMicroservice.Services;
using Commons.Consts;

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
        public ActionResult<List<FacultyResponseDTO>> HandleGetAllFaculties() {
            return Ok(this._facultyService.GetAll());
        }
    }
}