
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
        //obelezja
        private readonly ICourseService _courseService;

        //konstruktor
        public CourseController(ICourseService courseService)
        {
            this._courseService = courseService;
        }
        
        //GET METODE
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<CourseResponseDTO>> HandleGetAllCourses()
        {
            return Ok(this._courseService.GetAll());
        }
        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_COURSE_GET_ONE_BY_UUID)]
        public ActionResult<CourseResponseDTO>HandleGetOneCourseByUuid(string uuid)
        {
            return Ok(this._courseService.GetOneByUuid(uuid));
        }

        //POST METODE
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<CourseResponseDTO>HandleCreateCourse(CreateCourseRequestDTO requestDTO)
        {
            return Ok(this._courseService.Create(requestDTO));
        }

        //PUT METODE
        [AllowAnonymous]
        [HttpPut]
        public ActionResult<CourseResponseDTO>HandleUpdateCourse(UpdateCourseRequestDTO requestDTO)
        {
            return Ok(this._courseService.Update(requestDTO));
        }

        //DELETE METODE
        [AllowAnonymous]
        [HttpDelete]
        public ActionResult<CourseResponseDTO>HandleDeleteCourse(string uuid)
        {
            return Ok(this._courseService.Delete(uuid));
        }
    }
}