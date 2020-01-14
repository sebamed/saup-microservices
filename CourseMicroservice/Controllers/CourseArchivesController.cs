
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
    [Route(RouteConsts.ROUTE_COURSE_ARCHIVES)]
    [ApiController]
    public class CourseArchivesController : ControllerBase
    {
        private readonly ICourseArchivesService _courseArchivesService;
        public CourseArchivesController(ICourseArchivesService courseArchivesService)
        {
            this._courseArchivesService = courseArchivesService;
        }
        [Authorize(Roles = Commons.Consts.RoleConsts.ROLE_USER)]
        [HttpGet]
        public ActionResult<List<CourseArchiveResponseDTO>>HandleGetAllCourseArchives(string uuid)
        {
            return Ok(this._courseArchivesService.GetAllCourseArchives(uuid));
        }
    }
}