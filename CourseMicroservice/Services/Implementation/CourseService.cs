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
        private ICourseArchivesService _courseArchivesService;

        public CourseService(QueryExecutor queryExecutor, IMapper autoMapper, ModelMapper modelMapper, SqlCommands sqlCommands, HttpClientService httpClientService, IHttpContextAccessor httpContextAccessor, ICourseArchivesService courseArchivesService)
        {
            this._autoMapper = autoMapper;
            this._queryExecutor = queryExecutor;
            this._sqlCommands = sqlCommands;
            this._modelMapper = modelMapper;
            this._httpClientService = httpClientService;
            this._httpContextAccessor = httpContextAccessor;
            this._courseArchivesService = courseArchivesService;
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
        public List<CourseMultipleResponseDTO> GetAll()
        {
            List<CourseMultipleResponseDTO> response = this._autoMapper.Map<List<CourseMultipleResponseDTO>>((this.FindAll()));
            return response;
        }


        public CourseResponseDTO GetOneByUuid(string uuid)
        {
            //provera da li postoji taj kurs
            CourseResponseDTO course = this._autoMapper.Map<CourseResponseDTO>(this.FindOneByUuidOrThrow(uuid));
            course.subject = this._httpClientService.SendRequest<SubjectResponseDTO>(HttpMethod.Get, "http://localhost:40006/api/subjects/" + course.subjectUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            course.archives = this._courseArchivesService.GetAllCourseArchives(course.uuid);
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
            Course course = new Course()
            {
                name = requestDTO.name,
                description = requestDTO.description,
                active = requestDTO.active,
                maxStudents = requestDTO.maxtudents,
                minStudents = requestDTO.minStudents,
                creationDate = requestDTO.creationDate,
                subjectUUID = requestDTO.subjectUUID
            };
            this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_COURSE(course), this._modelMapper.MapToCourseAfterInsert);
            CourseArchive archive = new CourseArchive()
            {
                courseUUID = course.uuid,
                name = course.name,
                description = course.description,
                active = course.active,
                maxStudents = course.maxStudents,
                minStudents = course.minStudents,
                creationDate = course.creationDate,
                subjectUUID = course.subjectUUID,
                changeDate = DateTime.Now,
                moderatorUUID = "test"
    };
            CreateCourseArchiveRequest req = this._autoMapper.Map<CreateCourseArchiveRequest>(archive);
            CourseArchiveResponseDTO archiveResponse = this._courseArchivesService.CreateCourseArchive(req);
            CourseResponseDTO response = this._autoMapper.Map<CourseResponseDTO>(course);
            response.subject = subject;
            response.archives = this._courseArchivesService.GetAllCourseArchives(course.uuid);
            return response;
        }

        //PUT METHODS
        public CourseResponseDTO Update(UpdateCourseRequestDTO requestDTO)
        {
            //provera da li postoji kurs
            Course oldCourse = this.FindOneByUuidOrThrow(requestDTO.uuid);
            //update u tabeli kurs
            Course course = new Course()
            {
                uuid = requestDTO.uuid,
                name = requestDTO.name,
                description = requestDTO.description,
                active = requestDTO.active,
                maxStudents = requestDTO.maxtudents,
                minStudents = requestDTO.minStudents,
                creationDate = requestDTO.creationDate,
                subjectUUID = oldCourse.subjectUUID
            };
            this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_COURSE(course), this._modelMapper.MapToCourseAfterInsert);
            //insert u tabeli arhiva
            CourseArchive archive = new CourseArchive()
            {
                courseUUID = course.uuid,
                name = course.name,
                description = course.description,
                active = course.active,
                maxStudents = course.maxStudents,
                minStudents = course.minStudents,
                creationDate = course.creationDate,
                subjectUUID = course.subjectUUID,
                changeDate = DateTime.Now,
                moderatorUUID = "test"
            };
            CreateCourseArchiveRequest req = this._autoMapper.Map<CreateCourseArchiveRequest>(archive);
            CourseArchiveResponseDTO archiveResponse = this._courseArchivesService.CreateCourseArchive(req);
            CourseResponseDTO response = this._autoMapper.Map<CourseResponseDTO>(course);
            response.subject = this._httpClientService.SendRequest<SubjectResponseDTO>(HttpMethod.Get, "http://localhost:40006/api/subjects/" + response.subjectUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            response.archives = this._courseArchivesService.GetAllCourseArchives(course.uuid);
            return response;
        }

        //DELETE METHODS
        public CourseResponseDTO Delete(string uuid)
        {
            //provera da li postoji kurs
            Course course = this.FindOneByUuidOrThrow(uuid);
            course = this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_COURSE(uuid), this._modelMapper.MapToCourse);
            CourseResponseDTO response = this._autoMapper.Map<CourseResponseDTO>(course);
            response.subject = this._httpClientService.SendRequest<SubjectResponseDTO>(HttpMethod.Get, "http://localhost:40006/api/subjects/" + response.subjectUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            response.archives = this._courseArchivesService.GetAllCourseArchives(response.uuid);
            return response;
        }



    }
}
