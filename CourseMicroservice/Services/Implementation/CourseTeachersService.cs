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
    public class CourseTeacherService : ICourseTeacherService
    {
        private readonly IMapper _autoMapper;
        private readonly QueryExecutor _queryExecutor;
        private readonly SqlCommands _sqlCommands;
        private readonly ModelMapper _modelMapper;
        private readonly HttpClientService _httpClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourseTeacherService(QueryExecutor queryExecutor, IMapper autoMapper, ModelMapper modelMapper, SqlCommands sqlCommands, HttpClientService httpClientService, IHttpContextAccessor httpContextAccessor)
        {
            this._autoMapper = autoMapper;
            this._queryExecutor = queryExecutor;
            this._sqlCommands = sqlCommands;
            this._modelMapper = modelMapper;
            this._httpClientService = httpClientService;
            this._httpContextAccessor = httpContextAccessor;
        }
        //HELPER METHODS
        public Course FindOneByUuidOrThrow(string uuid)
        {
            Course course = this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_COURSE_BY_UUID(uuid), this._modelMapper.MapToCourse);
            if (course == null)
            {
                throw new EntityNotFoundException($"Course with uuid: {uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }
            return course;
        }
        public CourseTeacherResponseDTO connectWithUser(CourseTeacherResponseDTO response)
        {
            TeacherResponseDTO newTeacher = this._httpClientService.SendRequest<TeacherResponseDTO>(HttpMethod.Get, "http://localhost:40001/api/users/" + response.teacherUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            response.name = newTeacher.name;
            response.surname = newTeacher.surname;
            response.email = newTeacher.email;
            return response;
        }
        public List<CourseTeacher> FindAllTeachersOnCourse(string uuid)
        {
            return this._queryExecutor.Execute<List<CourseTeacher>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_COURSE_TEACHERS(uuid), this._modelMapper.MapToCourseTeachers);
        }
        public CourseTeacher FindTeacherOnCourseOrThrow(string courseUUID, string teacherUUID)
        {
            CourseTeacher teacher = this._queryExecutor.Execute<CourseTeacher>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_COURSE_TEACHER(courseUUID, teacherUUID), this._modelMapper.MapToCourseTeacher);
            if(teacher == null)
            {
                throw new EntityNotFoundException($"Teacher with uuid: {teacherUUID} in course with uuid: {courseUUID} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }
            return teacher;
        }
        public CourseTeacher FindTeacherOnCourse(string courseUUID, string teacherUUID)
        {
            CourseTeacher teacher = this._queryExecutor.Execute<CourseTeacher>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_COURSE_TEACHER(courseUUID, teacherUUID), this._modelMapper.MapToCourseTeacher);
            return teacher;
        }

        //GET METHODS
        public List<CourseTeacherResponseDTO> GetAllTeachersOnCourse(string uuid)
        {
            //provera da li postoji kurs
            this.FindOneByUuidOrThrow(uuid); 
            List<CourseTeacher> teachers = FindAllTeachersOnCourse(uuid);
            List<CourseTeacherResponseDTO> response = this._autoMapper.Map<List<CourseTeacherResponseDTO>>(teachers);
            for(int i=0; i< response.Count; i++)
            {
                response[i] = connectWithUser(response[i]);
            }
            return response;

        }

        //PUT METHODS
        public CourseTeacherResponseDTO UpdateTeacherOnCourse(string uuid, CourseTeacherRequestDTO request)
        {
            //provera da li postoji profesor na kursu
            CourseTeacher oldCourseTeacher = FindTeacherOnCourseOrThrow(uuid, request.teacherUUID);
            CourseTeacher newCourseTeacher = this._autoMapper.Map<CourseTeacher>(request);
            newCourseTeacher.courseUUID = oldCourseTeacher.courseUUID;

            newCourseTeacher = this._queryExecutor.Execute<CourseTeacher>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_TEACHER_COURSE(newCourseTeacher), this._modelMapper.MapToCourseTeacher);

            CourseTeacherResponseDTO response = this._autoMapper.Map<CourseTeacherResponseDTO>(newCourseTeacher);
            return connectWithUser(response);
        }

        //DELETE METHODS
        public CourseTeacherResponseDTO DeleteTeacherOnCourse(string uuid, string teacherUUID)
        {
            //provera da li postoji profesor na kursu
            CourseTeacher courseTeacher = FindTeacherOnCourseOrThrow(uuid, teacherUUID);
            courseTeacher = this._queryExecutor.Execute<CourseTeacher>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_TEACHER_COURSE(uuid, teacherUUID), this._modelMapper.MapToCourseTeacher);
            CourseTeacherResponseDTO response = this._autoMapper.Map<CourseTeacherResponseDTO>(courseTeacher);
            return connectWithUser(response);
        }

        public CourseTeacherResponseDTO CreateTeacherOnCourse(string courseUUID, CourseTeacherRequestDTO request)
        {
            //provera da li postoji kurs
            Course course = FindOneByUuidOrThrow(courseUUID);
            //provera da li postoji profesor
            TeacherResponseDTO newTeacher = this._httpClientService.SendRequest<TeacherResponseDTO>(HttpMethod.Get, "http://localhost:40001/api/users/" + request.teacherUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            if(newTeacher == null)
            {
                throw new EntityNotFoundException($"Teacher with uuid: {request.teacherUUID} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }
            //provera da li vec postoji profesor na kursu
            CourseTeacher existingCourseTeacher = FindTeacherOnCourseOrThrow(courseUUID, request.teacherUUID);
            if (existingCourseTeacher != null)
            {
                throw new EntityAlreadyExistsException("Teacher with uuid " + courseUUID + " already exists in course with uuid " + request.teacherUUID, GeneralConsts.MICROSERVICE_NAME);
            }
            CourseTeacher courseTeacher = this._autoMapper.Map<CourseTeacher>(request);
            this._queryExecutor.Execute<CourseTeacher>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_TEACHER_COURSE(courseTeacher), this._modelMapper.MapToCourseTeacher);
            CourseTeacherResponseDTO response = this._autoMapper.Map<CourseTeacherResponseDTO>(courseTeacher);
            return connectWithUser(response);
        }
    }
}
