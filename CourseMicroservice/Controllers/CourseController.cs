
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
    [Route(RouteConsts.ROUTE_COURSE_BASE)]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            this._courseService = courseService;
        }
        //GET METHODS
        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet]
        public ActionResult<List<CourseMultipleResponseDTO>> HandleGetAllCourses()
        {
            return Ok(this._courseService.GetAll());
        }
        
        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_COURSE_GET_ONE_BY_UUID)]
        public ActionResult<CourseResponseDTO>HandleGetOneCourseByUuid(string uuid)
        {
            return Ok(this._courseService.GetOneByUuid(uuid));
        }
        
        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpPost]
        public ActionResult<CourseResponseDTO>HandleCreateCourse(CreateCourseRequestDTO requestDTO)
        {
            return Ok(this._courseService.Create(requestDTO));
        }
        
        //PUT METHODS
        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpPut]
        public ActionResult<CourseResponseDTO>HandleUpdateCourse(UpdateCourseRequestDTO requestDTO)
        {
            return Ok(this._courseService.Update(requestDTO));
        }
        
        //DELETE METHODS
        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        [HttpDelete(RouteConsts.ROUTE_COURSE_GET_ONE_BY_UUID)]
        public ActionResult<CourseResponseDTO>HandleDeleteCourse(string uuid)
        {
            return Ok(this._courseService.Delete(uuid));
        }
    }
}