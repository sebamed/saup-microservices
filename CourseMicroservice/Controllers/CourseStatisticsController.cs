
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
    [Route(RouteConsts.ROUTE_COURSE_STATISTICS)]
    [ApiController]
    public class CourseStatisticsController : ControllerBase
    {
        private readonly ICourseStatisticsService _courseStatisticsService;
        public CourseStatisticsController(ICourseStatisticsService courseStatisticsService)
        {
            this._courseStatisticsService = courseStatisticsService;
        }

        [HttpGet]
        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        public ActionResult<CourseStatisticsResponseDTO> HandleGetStatisticsByCourse(string courseuuid)
        {
           return  this._courseStatisticsService.Get_Course_Statistics(courseuuid);
        }
        

    }
}