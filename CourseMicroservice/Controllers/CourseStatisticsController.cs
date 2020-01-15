
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
    [Route(RouteConsts.ROUTE_COURSE_STATISTICS_BASE)]
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
        [Route(RouteConsts.ROUTE_COURSE_STATISTICS_BY_COURSE_UUID)]
        public ActionResult<CourseStatisticsResponseDTO> HandleGetStatisticsByCourse(string courseuuid)
        {
           return  this._courseStatisticsService.Get_Course_Statistics_Course_Uuid(courseuuid);
        }
        [HttpGet]
        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        [Route(RouteConsts.ROUTE_COURSE_STATISTICS_BY_STUDENT_UUID)]
        public ActionResult<CourseStatisticsResponseDTO> HandleGetStatisticsByStudent(string studentuuid)
        {
            return this._courseStatisticsService.Get_Course_Satistics_Student_Uuid(studentuuid);
        }
        [HttpGet]
        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        [Route(RouteConsts.ROUTE_COURSE_STATISTICS_BY_YEAR)]
        public ActionResult<CourseStatisticsResponseDTO> HandleGetStatisticsByYear(int year)
        {
            return this._courseStatisticsService.Get_Course_Statistics_Year(year);
        }



    }
}