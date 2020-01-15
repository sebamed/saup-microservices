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
using System;

namespace CourseMicroservice.Services.Implementation {
    public class CourseService : ICourseService
    {
        private readonly IMapper _autoMapper;
        private readonly QueryExecutor _queryExecutor;
        private readonly SqlCommands _sqlCommands;
        private readonly ModelMapper _modelMapper;
        private readonly HttpClientService _httpClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICourseArchivesService _courseArchiveService;

        public CourseService(ICourseArchivesService courseArchiveService, QueryExecutor queryExecutor, IMapper autoMapper, ModelMapper modelMapper, SqlCommands sqlCommands, HttpClientService httpClientService, IHttpContextAccessor httpContextAccessor)
        {
            this._autoMapper = autoMapper;
            this._queryExecutor = queryExecutor;
            this._sqlCommands = sqlCommands;
            this._modelMapper = modelMapper;
            this._httpClientService = httpClientService;
            this._httpContextAccessor = httpContextAccessor;
            this._courseArchiveService = courseArchiveService;
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
                throw new EntityNotFoundException($"Course with uuid: {uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);

            return course;
        }

        //GET METHODS
        public List<CourseMultipleResponseDTO> GetAll() {
            return this._autoMapper.Map<List<CourseMultipleResponseDTO>>((this.FindAll()));
        }

        public CourseResponseDTO GetOneByUuid(string uuid)
        {
            //provera da li postoji taj kurs
            CourseResponseDTO course = this._autoMapper.Map<CourseResponseDTO>(this.FindOneByUuidOrThrow(uuid));
            try {
                course.subject = this._httpClientService.SendRequest<SubjectResponseDTO>(HttpMethod.Get, "http://localhost:40006/api/subjects/" + course.subject.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            } catch {
                throw new EntityNotFoundException($"Subject with uuid: {course.subject.uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }

            return course;
        }

        //POST METHODS
        CourseResponseDTO ICourseService.Create(CreateCourseRequestDTO requestDTO)
        {
            //provera da li postoji subject
            SubjectResponseDTO subject;
            try
            {
                subject = this._httpClientService.SendRequest<SubjectResponseDTO>(HttpMethod.Get, "http://localhost:40006/api/subjects/" + requestDTO.subjectUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            }
            catch
            {
                throw new EntityNotFoundException("Subject with uuid " + requestDTO.subjectUUID + " doesn't exist", GeneralConsts.MICROSERVICE_NAME);
            }

            Course course = new Course()
            {
                name = requestDTO.name,
                description = requestDTO.description,
                active = requestDTO.active,
                maxStudents = requestDTO.maxStudents,
                minStudents = requestDTO.minStudents,
                creationDate = requestDTO.creationDate,
                subject = new Subject()
                {
                    uuid = requestDTO.subjectUUID
                }
            };

            this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_COURSE(course), this._modelMapper.MapToCourseAfterInsert);

            CreateCourseArchiveRequestDTO archive = new CreateCourseArchiveRequestDTO()
            {
                courseUUID = course.uuid,
                name = course.name,
                description = course.description,
                active = course.active,
                maxStudents = course.maxStudents,
                minStudents = course.minStudents,
                creationDate = course.creationDate,
                subjectUUID = subject.uuid,
                changeDate = DateTime.Now,
                moderatorUUID = new UserPrincipal(_httpContextAccessor.HttpContext).uuid
            };

            CreateCourseArchiveRequestDTO req = this._autoMapper.Map<CreateCourseArchiveRequestDTO>(archive);
            _ = this._courseArchiveService.CreateCourseArchive(archive);
            CourseResponseDTO response = this._autoMapper.Map<CourseResponseDTO>(course);
            response.subject = subject;
            return response;
        }

        //PUT METHODS
        public CourseResponseDTO Update(UpdateCourseRequestDTO requestDTO)
        {
            //provera da li postoji kurs
            Course oldCourse = this.FindOneByUuidOrThrow(requestDTO.uuid);
            SubjectResponseDTO subject;
            try
            {
               subject  = this._httpClientService.SendRequest<SubjectResponseDTO>(HttpMethod.Get, "http://localhost:40006/api/subjects/" + requestDTO.subjectUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            }
            catch
            {
                    throw new EntityNotFoundException("Subject with uuid " + requestDTO.subjectUUID + " doesn't exist", GeneralConsts.MICROSERVICE_NAME);
            }

            //update u tabeli kurs
            Course course = new Course()
            {
                uuid = requestDTO.uuid,
                name = requestDTO.name,
                description = requestDTO.description,
                active = requestDTO.active,
                maxStudents = requestDTO.maxStudents,
                minStudents = requestDTO.minStudents,
                creationDate = requestDTO.creationDate,
                subject = new Subject()
                {
                    uuid = requestDTO.subjectUUID
                }
            };
            this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_COURSE(course), this._modelMapper.MapToCourseAfterInsert);
            //insert u tabeli arhiva
            CreateCourseArchiveRequestDTO archive = new CreateCourseArchiveRequestDTO()
            {
                courseUUID = course.uuid,
                name = course.name,
                description = course.description,
                active = course.active,
                maxStudents = course.maxStudents,
                minStudents = course.minStudents,
                creationDate = course.creationDate,
                subjectUUID = subject.uuid,
                changeDate = DateTime.Now,
                moderatorUUID = new UserPrincipal(_httpContextAccessor.HttpContext).uuid
            };

            _ = this._courseArchiveService.CreateCourseArchive(archive);
            CourseResponseDTO response = this._autoMapper.Map<CourseResponseDTO>(course);
            response.subject = subject;
            return response;
        }

        //DELETE METHODS
        public CourseResponseDTO Delete(string uuid)
        {
            //provera da li postoji kurs
            Course course = this.FindOneByUuidOrThrow(uuid);
            course = this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_COURSE(uuid), this._modelMapper.MapToCourse);
            CourseResponseDTO response = this._autoMapper.Map<CourseResponseDTO>(course);
            response.subject = this._httpClientService.SendRequest<SubjectResponseDTO>(HttpMethod.Get, "http://localhost:40006/api/subjects/" + response.subject.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
        }
    }
}
