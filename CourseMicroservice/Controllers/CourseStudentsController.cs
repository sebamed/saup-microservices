﻿
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
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<CourseStudentResponseDTO>> HandleGetAllStudentsOnCourse(string uuid)
        {
            return Ok(this._courseStudentsService.GetAllStudentsOnCourse(uuid));
        }
        //PUT METHODS
        [AllowAnonymous]
        [HttpPut]
        public ActionResult<CourseStudentResponseDTO> HandleUpdateStudentOnCourse(string uuid, CourseStudentRequestDTO request)
        {
            return Ok(this._courseStudentsService.UpdateStudentOnCourse(uuid, request));
        }
        //POST METHODS
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<CourseResponseDTO>HandleCreateStudentOnCourse(string uuid, CourseStudentRequestDTO request)
        {
            return Ok(this._courseStudentsService.CreateStudentOnCourse(uuid, request));
        }
        //DELETE METHODS
        [AllowAnonymous]
        [HttpDelete(RouteConsts.ROUTE_COURSE_STUDENTS_GET_ONE_BY_UUID)]
        public ActionResult<CourseStudentResponseDTO> HandleDeleteStudentOnCourse(string uuid, string studentUuid)
        {
            return Ok(this._courseStudentsService.DeleteStudentOnCourse(uuid, studentUuid));
        }
    }
}