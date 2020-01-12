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
        private ICourseStudentsService _courseStudentsService;

        public CourseService(QueryExecutor queryExecutor, IMapper autoMapper, ModelMapper modelMapper, SqlCommands sqlCommands, HttpClientService httpClientService, IHttpContextAccessor httpContextAccessor, ICourseTeacherService courseTeacherService, ICourseStudentsService courseStudentsService)
        {
            this._autoMapper = autoMapper;
            this._queryExecutor = queryExecutor;
            this._sqlCommands = sqlCommands;
            this._modelMapper = modelMapper;
            this._httpClientService = httpClientService;
            this._httpContextAccessor = httpContextAccessor;
            this._courseTeacherService = courseTeacherService;
            this._courseStudentsService = courseStudentsService;
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
        public Course FindOneByUuid(string uuid)
        {
            Course course = this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_COURSE_BY_UUID(uuid), this._modelMapper.MapToCourse);
            return course;
        }


        //GET METHODS
        public List<CourseResponseDTO> GetAll()
        {
            List<CourseResponseDTO> response = this._autoMapper.Map<List<CourseResponseDTO>>((this.FindAll()));
            for (int i = 0; i<response.Count; i++)
            {
                response[i].teachers = this._courseTeacherService.GetAllTeachersOnCourse(response[i].uuid);
                response[i].students = this._courseStudentsService.GetAllStudentsOnCourse(response[i].uuid);
                response[i].subject = this._httpClientService.SendRequest<SubjectResponseDTO>(HttpMethod.Get, "http://localhost:40006/api/subjects/" + response[i].subjectUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            }
            return response;
        }


        public CourseResponseDTO GetOneByUuid(string uuid)
        {
            //provera da li postoji taj kurs
            CourseResponseDTO course = this._autoMapper.Map<CourseResponseDTO>(this.FindOneByUuidOrThrow(uuid));
            course.teachers = this._courseTeacherService.GetAllTeachersOnCourse(course.uuid);
            course.students = this._courseStudentsService.GetAllStudentsOnCourse(course.uuid);
            course.subject = this._httpClientService.SendRequest<SubjectResponseDTO>(HttpMethod.Get, "http://localhost:40006/api/subjects/" + course.subjectUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return course;
        }


        //POST METHODS
        CourseResponseDTO ICourseService.Create(CreateCourseRequestDTO requestDTO)
        {
            //provera da li postoji subject
            SubjectResponseDTO subject = this._httpClientService.SendRequest<SubjectResponseDTO>(HttpMethod.Get, "http://localhost:40006/api/subjects/" + requestDTO.subjectUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            if(subject == null)
            {
                throw new EntityNotFoundException("Subject with uuid " + requestDTO.subjectUUID + " doesn't exist", GeneralConsts.MICROSERVICE_NAME);
            }
            Course course = this._autoMapper.Map<Course>(requestDTO);
            this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_COURSE(course), this._modelMapper.MapToCourseAfterInsert);
            CourseResponseDTO response = this._autoMapper.Map<CourseResponseDTO>(course);
            response.subject = subject;
            return response;
        }

        //PUT METHODS
        public CourseResponseDTO Update(UpdateCourseRequestDTO requestDTO)
        {
            //provera da li postoji kurs
            Course course = this.FindOneByUuidOrThrow(requestDTO.uuid);
            course = this._autoMapper.Map<Course>(requestDTO);
            course = this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_COURSE(course), this._modelMapper.MapToCourseAfterUpdate);
            CourseResponseDTO response = this._autoMapper.Map<CourseResponseDTO>(course);
            response.teachers = _courseTeacherService.GetAllTeachersOnCourse(course.uuid);
            response.students = this._courseStudentsService.GetAllStudentsOnCourse(course.uuid);
            response.subject = this._httpClientService.SendRequest<SubjectResponseDTO>(HttpMethod.Get, "http://localhost:40006/api/subjects/" + response.subjectUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
        }

        //DELETE METHODS
        public CourseResponseDTO Delete(string uuid)
        {
            //provera da li postoji kurs
            Course course = this.FindOneByUuidOrThrow(uuid);
            course = this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_COURSE(uuid), this._modelMapper.MapToCourse);
            CourseResponseDTO response = this._autoMapper.Map<CourseResponseDTO>(course);
            response.teachers = _courseTeacherService.GetAllTeachersOnCourse(course.uuid);
            response.students = this._courseStudentsService.GetAllStudentsOnCourse(course.uuid);
            response.subject = this._httpClientService.SendRequest<SubjectResponseDTO>(HttpMethod.Get, "http://localhost:40006/api/subjects/" + response.subjectUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
        }



    }
}
