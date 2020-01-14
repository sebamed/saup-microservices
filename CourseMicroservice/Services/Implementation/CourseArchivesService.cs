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
    public class CourseArchivesService : ICourseArchivesService
    {
        private readonly IMapper _autoMapper;
        private readonly QueryExecutor _queryExecutor;
        private readonly SqlCommands _sqlCommands;
        private readonly ModelMapper _modelMapper;
        private readonly HttpClientService _httpClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICourseService _courseService;

        public CourseArchivesService(QueryExecutor queryExecutor, IMapper autoMapper, ModelMapper modelMapper, SqlCommands sqlCommands, HttpClientService httpClientService, IHttpContextAccessor httpContextAccessor, ICourseService courseService)
        {
            this._autoMapper = autoMapper;
            this._queryExecutor = queryExecutor;
            this._sqlCommands = sqlCommands;
            this._modelMapper = modelMapper;
            this._httpClientService = httpClientService;
            this._httpContextAccessor = httpContextAccessor;
            this._courseService = courseService;
        }

        public CourseArchiveResponseDTO CreateCourseArchive(CreateCourseArchiveRequestDTO request)
        {
            CourseArchive archive = this._autoMapper.Map<CourseArchive>(request);
            archive = this._queryExecutor.Execute<CourseArchive>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_COURSE_ARCHIVE(archive), this._modelMapper.MapToCourseArchive);
            return this._autoMapper.Map<CourseArchiveResponseDTO>(archive);
        }
        
        //GET METHODS
        public List<CourseArchiveResponseDTO> GetAllCourseArchives(string uuid)
        {
            //provera da li postoji kurs
            this._courseService.GetOneByUuid(uuid);
            List<CourseArchive> archives = this._queryExecutor.Execute<List<CourseArchive>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_COURSE_ARCHIVES(uuid), this._modelMapper.MapToCourseArchives);
            return this._autoMapper.Map<List<CourseArchiveResponseDTO>>(archives);
        }
    }
}
