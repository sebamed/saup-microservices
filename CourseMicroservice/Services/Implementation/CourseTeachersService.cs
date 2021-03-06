﻿using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using System.Collections.Generic;
using CourseMicroservice.Consts;
using CourseMicroservice.Domain;
using CourseMicroservice.Mappers;
using CourseMicroservice.DTO.Course;
using AutoMapper;
using Commons.HttpClientRequests;
using System.Net.Http;
using Commons.Domain;
using Microsoft.AspNetCore.Http;

namespace CourseMicroservice.Services.Implementation {
    public class CourseTeacherService : ICourseTeacherService
    {
        private readonly IMapper _autoMapper;
        private readonly QueryExecutor _queryExecutor;
        private readonly SqlCommands _sqlCommands;
        private readonly ModelMapper _modelMapper;
        private readonly HttpClientService _httpClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICourseService _courseService;

        public CourseTeacherService(QueryExecutor queryExecutor, IMapper autoMapper, ModelMapper modelMapper, SqlCommands sqlCommands, HttpClientService httpClientService, IHttpContextAccessor httpContextAccessor, ICourseService courseService)
        {
            this._autoMapper = autoMapper;
            this._queryExecutor = queryExecutor;
            this._sqlCommands = sqlCommands;
            this._modelMapper = modelMapper;
            this._httpClientService = httpClientService;
            this._httpContextAccessor = httpContextAccessor;
            this._courseService = courseService;
        }
        //HELPER METHODS
        public CourseTeacherResponseDTO connectWithUser(CourseTeacherResponseDTO response)
        {
            TeacherResponseDTO newTeacher;
            try
            {
                newTeacher = this._httpClientService.SendRequest<TeacherResponseDTO>(HttpMethod.Get, "http://localhost:40001/api/users/" + response.teacher.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            }
            catch
            {
                throw new EntityNotFoundException($"Teacher with uuid: {response.teacher.uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }

            response.teacher = newTeacher;
            response.course = this._courseService.GetOneByUuid(response.course.uuid);
            return response;
        }
        public List<CourseTeacher> FindAllTeachersOnCourse(string uuid)
        {
            return this._queryExecutor.Execute<List<CourseTeacher>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ACTIVE_COURSE_TEACHERS(uuid), this._modelMapper.MapToCourseTeachers);
        }
        public CourseTeacher FindTeacherOnCourseOrThrow(string courseUUID, string teacherUUID)
        {
            CourseTeacher teacher = this._queryExecutor.Execute<CourseTeacher>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_COURSE_TEACHER(courseUUID, teacherUUID), this._modelMapper.MapToCourseTeacher);
            if(teacher == null)
                throw new EntityNotFoundException($"Teacher with uuid: {teacherUUID} in course with uuid: {courseUUID} does not exist!", GeneralConsts.MICROSERVICE_NAME);

            return teacher;
        }
        public CourseTeacher FindTeacherOnCourse(string courseUUID, string teacherUUID)
        {
            CourseTeacher teacher = this._queryExecutor.Execute<CourseTeacher>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_COURSE_TEACHER(courseUUID, teacherUUID), this._modelMapper.MapToCourseTeacher);
            return teacher;
        }

        public List<CourseTeacherMultipleResponseDTO> GetAllActiveTeachersOnCourse(string uuid)
        {
            //provera da li postoji kurs
            this._courseService.GetOneByUuid(uuid);
            List<CourseTeacher> teachers = FindAllTeachersOnCourse(uuid);
            List<CourseTeacherMultipleResponseDTO> response = this._autoMapper.Map<List<CourseTeacherMultipleResponseDTO>>(teachers);
            return response;
        }

        public CourseTeacherResponseDTO CreateTeacherOnCourse(CreateCourseTeacherRequestDTO request)
        {
            //provera da li postoji kurs
            CourseResponseDTO course = this._courseService.GetOneByUuid(request.courseUUID);
            //provera da li postoji profesor
            try
            {
                TeacherResponseDTO newTeacher = this._httpClientService.SendRequest<TeacherResponseDTO>(HttpMethod.Get, "http://localhost:40001/api/users/" + request.teacherUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            }
            catch
            {
                throw new EntityNotFoundException($"Teacher with uuid: {request.teacherUUID} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }

            //provera da li vec postoji profesor na kursu
            CourseTeacher existingCourseTeacher = FindTeacherOnCourse(request.courseUUID, request.teacherUUID);
            if (existingCourseTeacher != null)
            {
                if (existingCourseTeacher.activeTeacher == false)
                {
                    CourseTeacher updatedTeacher = this._autoMapper.Map<CourseTeacher>(request);
                    updatedTeacher.activeTeacher = true;
                    updatedTeacher.course = new Course()
                    {
                        uuid = request.courseUUID
                    };
                    updatedTeacher.teacher = new Teacher()
                    {
                        uuid = request.teacherUUID
                    };
                    updatedTeacher = this._queryExecutor.Execute<CourseTeacher>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_TEACHER_COURSE(updatedTeacher), this._modelMapper.MapToCourseTeacher);
                    CourseTeacherResponseDTO updatedResponse = this._autoMapper.Map<CourseTeacherResponseDTO>(updatedTeacher);
                    return connectWithUser(updatedResponse);
                }
                else
                {
                    throw new EntityAlreadyExistsException("Teacher with uuid " + request.courseUUID + " already exists in course with uuid " + request.teacherUUID, GeneralConsts.MICROSERVICE_NAME);
                }

            }
            CourseTeacher courseTeacher = this._autoMapper.Map<CourseTeacher>(request);
            courseTeacher.course = new Course()
            {
                uuid = request.courseUUID
            };
            courseTeacher.teacher = new Teacher()
            {
                uuid = request.teacherUUID
            };
            courseTeacher.activeTeacher = true;
            this._queryExecutor.Execute<CourseTeacher>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_TEACHER_COURSE(courseTeacher), this._modelMapper.MapToCourseTeacher);
            CourseTeacherResponseDTO response = this._autoMapper.Map<CourseTeacherResponseDTO>(courseTeacher);
            return connectWithUser(response);
        }

        //PUT METHODS
        public CourseTeacherResponseDTO UpdateTeacherOnCourse(UpdateCourseTeacherRequestDTO request)
        {
            //provera da li postoji profesor na kursu
            CourseTeacher oldCourseTeacher = FindTeacherOnCourseOrThrow(request.courseUUID, request.teacherUUID);
            CourseTeacher newCourseTeacher = this._autoMapper.Map<CourseTeacher>(request);
            newCourseTeacher.course = new Course()
            {
                uuid = request.courseUUID
            };
            newCourseTeacher.teacher = new Teacher()
            {
                uuid = request.teacherUUID
            };

            newCourseTeacher = this._queryExecutor.Execute<CourseTeacher>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_TEACHER_COURSE(newCourseTeacher), this._modelMapper.MapToCourseTeacher);

            CourseTeacherResponseDTO response = this._autoMapper.Map<CourseTeacherResponseDTO>(newCourseTeacher);
            return connectWithUser(response);
        }

        //DELETE METHODS
        public CourseTeacherResponseDTO DeleteTeacherOnCourse(string uuid, string teacherUUID)
        {
            //provera da li postoji profesor na kursu
            CourseTeacher courseTeacher = FindTeacherOnCourseOrThrow(uuid, teacherUUID);
            courseTeacher.activeTeacher = false;
            courseTeacher = this._queryExecutor.Execute<CourseTeacher>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_TEACHER_COURSE(courseTeacher), this._modelMapper.MapToCourseTeacher);
            CourseTeacherResponseDTO response = this._autoMapper.Map<CourseTeacherResponseDTO>(courseTeacher);
            return connectWithUser(response);
        }
    }
}
