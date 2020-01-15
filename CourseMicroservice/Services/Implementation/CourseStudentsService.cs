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
    public class CourseStudentsService : ICourseStudentsService
    {
        private readonly IMapper _autoMapper;
        private readonly QueryExecutor _queryExecutor;
        private readonly SqlCommands _sqlCommands;
        private readonly ModelMapper _modelMapper;
        private readonly HttpClientService _httpClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICourseService _courseService;

        public CourseStudentsService(QueryExecutor queryExecutor, IMapper autoMapper, ModelMapper modelMapper, SqlCommands sqlCommands, HttpClientService httpClientService, IHttpContextAccessor httpContextAccessor, ICourseService courseService)
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
        public CourseStudent FindStudentOnCourseOrThrow(string courseUuid, string studentUuid)
        {
            CourseStudent courseStudent = this._queryExecutor.Execute<CourseStudent>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_COURSE_STUDENT(courseUuid, studentUuid), this._modelMapper.MapToCourseStudent);
            if(courseStudent == null)
                throw new EntityNotFoundException($"Student with uuid: {studentUuid} in course with uuid: {courseUuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);

            return courseStudent;
        }

        public CourseStudent FindStudentOnCourse(string courseUuid, string studentUuid)
        {
            return this._queryExecutor.Execute<CourseStudent>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_COURSE_STUDENT(courseUuid, studentUuid), this._modelMapper.MapToCourseStudent);
        }

        public CourseStudentResponseDTO connectWithUser(CourseStudentResponseDTO response) {
            StudentResponseDTO student = this._httpClientService.SendRequest<StudentResponseDTO>(HttpMethod.Get, "http://localhost:40001/api/users/students/" + response.student.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            if (student == null)
                throw new EntityNotFoundException($"Student with uuid: {student.uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);

            response.student = student;
            return response;
        }

        public List<CourseStudentMultipleResponseDTO> GetAllActiveStudentsOnCourse(string uuid)
        {
            //provera da li postoji kurs
            this._courseService.GetOneByUuid(uuid);
            List<CourseStudent> activeCourseStudents = this._queryExecutor.Execute<List<CourseStudent>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ACTIVE_COURSE_STUDENTS(uuid), this._modelMapper.MapToCourseStudents);
            return this._autoMapper.Map<List<CourseStudentMultipleResponseDTO>>(activeCourseStudents);
        }
        

        public CourseStudentResponseDTO CreateStudentOnCourse(CreateCourseStudentRequestDTO request)
        {
            //provera da li postoji kurs
            CourseResponseDTO course = this._courseService.GetOneByUuid(request.courseUUID);
            //provera da li postoji student
            StudentResponseDTO student = this._httpClientService.SendRequest<StudentResponseDTO>(HttpMethod.Get, "http://localhost:40001/api/users/students/" + request.studentUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            if (student == null)
                throw new EntityNotFoundException("Student with uuid " + request.studentUUID + " doesn't exist", GeneralConsts.MICROSERVICE_NAME);

            //provera da li vec postoji student na tom kursu
            CourseStudent existingCourseStudent = this.FindStudentOnCourse(request.courseUUID, request.studentUUID);
            if(existingCourseStudent != null)
            {
                if (existingCourseStudent.activeStudent == false)
                {
                    CourseStudent updatedStudent = this._autoMapper.Map<CourseStudent>(request);
                    updatedStudent.activeStudent = true;
                    updatedStudent.course = new Course()
                    {
                        uuid = request.courseUUID
                    };
                    updatedStudent.student = new Student()
                    {
                        uuid = request.studentUUID
                    };
                    updatedStudent = this._queryExecutor.Execute<CourseStudent>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_STUDENT_ON_COURSE(updatedStudent), this._modelMapper.MapToCourseStudent);
                    CourseStudentResponseDTO updatedResponse = this._autoMapper.Map<CourseStudentResponseDTO>(updatedStudent);
                    return connectWithUser(updatedResponse);
                }
                else
                {
                    throw new EntityAlreadyExistsException("Student with uuid " + request.studentUUID + " already exists in course with uuid " + request.courseUUID, GeneralConsts.MICROSERVICE_NAME);
                }
            }
            CourseStudent newCourseStudent = this._autoMapper.Map<CourseStudent>(request);
            newCourseStudent.activeStudent = true;
            newCourseStudent.course = new Course()
            {
                uuid = request.courseUUID
            };
            newCourseStudent.student = new Student()
            {
                uuid = request.studentUUID
            };
            newCourseStudent = this._queryExecutor.Execute<CourseStudent>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_STUDENT_ON_COURSE(newCourseStudent), this._modelMapper.MapToCourseStudent);
            CourseStudentResponseDTO response = this._autoMapper.Map<CourseStudentResponseDTO>(newCourseStudent);
            response.course = this._courseService.GetOneByUuid(response.course.uuid);
            return connectWithUser(response);
        }

        public CourseStudentResponseDTO UpdateStudentOnCourse(UpdateCourseStudentRequestDTO request)
        {
            //provera da li postojji student na kursu
            CourseStudent oldStudent = FindStudentOnCourseOrThrow(request.courseUUID, request.studentUUID);
            oldStudent.currentPoints = request.currentPoints;
            oldStudent.course = new Course()
            {
                uuid = request.courseUUID
            };
            oldStudent.student = new Student()
            {
                uuid = request.studentUUID
            };
            oldStudent.finalMark = request.finalMark;
            oldStudent.beginDate = request.beginDate;
            oldStudent = this._queryExecutor.Execute<CourseStudent>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_STUDENT_ON_COURSE(oldStudent), this._modelMapper.MapToCourseStudent);
            CourseStudentResponseDTO response = this._autoMapper.Map<CourseStudentResponseDTO>(oldStudent);
            response.course = this._courseService.GetOneByUuid(response.course.uuid);
            return connectWithUser(response);
        }

        public CourseStudentResponseDTO DeleteStudentOnCourse(string courseUuid, string studentUuid)
        {
            //provera da li postojji student na kursu
            CourseStudent oldStudent = FindStudentOnCourseOrThrow(courseUuid, studentUuid);
            oldStudent.activeStudent = false;
            oldStudent = this._queryExecutor.Execute<CourseStudent>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_STUDENT_ON_COURSE(oldStudent), this._modelMapper.MapToCourseStudent);
            CourseStudentResponseDTO response = this._autoMapper.Map<CourseStudentResponseDTO>(oldStudent);
            return connectWithUser(response);
        }

        public List<CourseStudentMultipleResponseDTO> GetAllCoursesByStudentUuid(string studentUuid)
        {
            //provera da li postoji student
            StudentResponseDTO student = this._httpClientService.SendRequest<StudentResponseDTO>(HttpMethod.Get, "http://localhost:40001/api/users/students/" + studentUuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            if (student == null)
                throw new EntityNotFoundException("Student with uuid " + studentUuid + " doesn't exist", GeneralConsts.MICROSERVICE_NAME);
            List<CourseStudent> courses = this._queryExecutor.Execute<List<CourseStudent>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_COURSES_FROM_STUDENT(studentUuid), this._modelMapper.MapToCourseStudents);
            return this._autoMapper.Map<List<CourseStudentMultipleResponseDTO>>(courses);
        }
    }
}
