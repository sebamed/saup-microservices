
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
    [Route(RouteConsts.ROUTE_COURSE_STUDENTS)]
    [ApiController]
    public class CourseStudentsController : ControllerBase
    {
        private readonly ICourseStudentsService _courseStudentsService;
        public CourseStudentsController(ICourseStudentsService courseStudentsService)
        {
            this._courseStudentsService = courseStudentsService;
        }

        //GET METHODS
        [Route(RouteConsts.ROUTE_COURSE_STUDENTS_BY_UUID)]
        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet]
        public ActionResult<List<CourseStudentMultipleResponseDTO>> HandleGetAllActiveStudentsOnCourse(string uuid)
        {
            return Ok(this._courseStudentsService.GetAllActiveStudentsOnCourse(uuid));
        }
        [Route(RouteConsts.ROUTE_COURSE_STUDENTS_BY_STUDENT_UUID)]
        [Authorize(Roles =RoleConsts.ROLE_USER)]
        [HttpGet]
        public ActionResult<List<CourseStudentMultipleResponseDTO>> HadleGetAllCoursesForStudent(string studentuuid)
        {
            return Ok(this._courseStudentsService.GetAllCoursesByStudentUuid(studentuuid));
        }
        
        //POST METHODS
        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpPost]
        public ActionResult<CourseStudentResponseDTO>HandleCreateStudentOnCourse(CreateCourseStudentRequestDTO request)
        {
            return Ok(this._courseStudentsService.CreateStudentOnCourse(request));
        }
        
        //PUT methods
        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpPut]
        public ActionResult<CourseStudentResponseDTO>HandleUpdateStudentOnCourse(UpdateCourseStudentRequestDTO requestDTO)
        {
            return Ok(this._courseStudentsService.UpdateStudentOnCourse(requestDTO));
        }

        //DELETE METHODS
        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpDelete]
        public ActionResult<CourseStudentResponseDTO> HandleDeleteStudentOnCourse(string uuid, string studentUuid)
        {
            return Ok(this._courseStudentsService.DeleteStudentOnCourse(uuid, studentUuid));
        }
    }
}