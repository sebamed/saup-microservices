using Commons.Consts;
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
    public class CourseService : ICourseService
    {
        private readonly IMapper _autoMapper;
        private readonly QueryExecutor _queryExecutor;
        private readonly SqlCommands _sqlCommands;
        private readonly ModelMapper _modelMapper;
        private readonly HttpClientService _httpClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ICourseTeacherService _courseTeacherService;

        public CourseService(QueryExecutor queryExecutor, IMapper autoMapper, ModelMapper modelMapper, SqlCommands sqlCommands, HttpClientService httpClientService, IHttpContextAccessor httpContextAccessor, ICourseTeacherService courseTeacherService)
        {
            this._autoMapper = autoMapper;
            this._queryExecutor = queryExecutor;
            this._sqlCommands = sqlCommands;
            this._modelMapper = modelMapper;
            this._httpClientService = httpClientService;
            this._httpContextAccessor = httpContextAccessor;
            this._courseTeacherService = courseTeacherService;
        }

        //HELPER METHODS
        public List<Course> FindAll()
        {
            return this._queryExecutor.Execute<List<Course>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_COURSES(), this._modelMapper.MapToCourses);
        }

        public Course FindOneByUuidOrThrow(string uuid)
        {
            Course course = this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_COURSE_BY_UUID(uuid), this._modelMapper.MapToCourse);
            if (course == null)
            {
                throw new EntityNotFoundException($"Course with uuid: {uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }
            return course;
        }

        //GET METHODS
        public List<CourseResponseDTO> GetAll()
        {
            List<CourseResponseDTO> response = this._autoMapper.Map<List<CourseResponseDTO>>((this.FindAll()));
            for (int i = 0; i<response.Count; i++)
            {
                response[i].teachers = this._courseTeacherService.GetAllTeachersOnCourse(response[i].uuid);
            }
            return response;
        }


        public CourseResponseDTO GetOneByUuid(string uuid)
        {
            CourseResponseDTO course = this._autoMapper.Map<CourseResponseDTO>(this.FindOneByUuidOrThrow(uuid));
            course.teachers = this._courseTeacherService.GetAllTeachersOnCourse(course.uuid);
            return course;
        }


        //POST METHODS
        CourseResponseDTO ICourseService.Create(CreateCourseRequestDTO requestDTO)
        {
            Course course = new Course()
            {
                name = requestDTO.name,
                description = requestDTO.description,
                active = requestDTO.active,
                minStudents = requestDTO.minStudents,
                maxStudents = requestDTO.maxtudents,
                creationDate = requestDTO.creationDate,
                subjectUUID = requestDTO.subjectUUID
            };
            this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_COURSE(course), this._modelMapper.MapToCourseAfterInsert);
            CourseResponseDTO response = this._autoMapper.Map<CourseResponseDTO>(course);
            response.teachers = _courseTeacherService.GetAllTeachersOnCourse(course.uuid);
            return response;
        }

        //PUT METHODS

        public CourseResponseDTO Update(UpdateCourseRequestDTO requestDTO)
        {
            Course course = this.FindOneByUuidOrThrow(requestDTO.uuid);
            course = this._autoMapper.Map<Course>(requestDTO);
            course = this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_COURSE(course), this._modelMapper.MapToCourseAfterUpdate);
            CourseResponseDTO response = this._autoMapper.Map<CourseResponseDTO>(course);
            response.teachers = _courseTeacherService.GetAllTeachersOnCourse(course.uuid);
            return response;
        }

        //DELETE METHODS
        public CourseResponseDTO Delete(string uuid)
        {
            Course course = this.FindOneByUuidOrThrow(uuid);
            course = this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_COURSE(uuid), this._modelMapper.MapToCourse);
            CourseResponseDTO response = this._autoMapper.Map<CourseResponseDTO>(course);
            response.teachers = _courseTeacherService.GetAllTeachersOnCourse(course.uuid);
            return response;
        }



    }
}
