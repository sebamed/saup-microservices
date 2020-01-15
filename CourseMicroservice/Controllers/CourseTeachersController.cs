
using Commons.Consts;
using Commons.Domain;
using CourseMicroservice.DTO.Course;
using CourseMicroservice.Localization;
using CourseMicroservice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CourseMicroservice.Controllers
{
    [Authorize]
    [Route(RouteConsts.ROUTE_COURSE_TEACHERS)]
    [ApiController]
    public class CourseTeachersController : ControllerBase
    {
        private readonly ICourseTeacherService _courseTeacherService;
        public CourseTeachersController(ICourseTeacherService courseTeacherService)
        {
            this._courseTeacherService = courseTeacherService;
        }

        //GET METHODS
        [Route(RouteConsts.ROUTE_COURSE_TEACHERS_BY_UUID)]
        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet]
        public ActionResult<List<CourseTeacherResponseDTO>> HandleGetAllActiveTeachersOnCourse(string uuid)
        {
            return Ok(this._courseTeacherService.GetAllActiveTeachersOnCourse(uuid));
        }
        
        //POST METHODS
        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpPost]
        public ActionResult<CourseTeacherResponseDTO> HandleCreateTeacherOnCourse(CreateCourseTeacherRequestDTO request)
        {
            return Ok(this._courseTeacherService.CreateTeacherOnCourse(request));
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpPut]
        public ActionResult<CourseTeacherResponseDTO> HandleUpdateTeacherOnCourse(UpdateCourseTeacherRequestDTO request)
        {
            return Ok(this._courseTeacherService.UpdateTeacherOnCourse(request));
        }

        //DELETE METHODS
        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        [HttpDelete]
        public ActionResult<CourseTeacherResponseDTO> HandleDeleteTeacherOnCourse(string uuid, string teacherUuid)
        {
            return Ok(this._courseTeacherService.DeleteTeacherOnCourse(uuid, teacherUuid));
        }
    }
}