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
    public class CourseStatisticsService : ICourseStatisticsService
    {
        private readonly IMapper _autoMapper;
        private readonly QueryExecutor _queryExecutor;
        private readonly SqlCommands _sqlCommands;
        private readonly ModelMapper _modelMapper;
        private readonly HttpClientService _httpClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICourseService _courseService;

        public CourseStatisticsService(QueryExecutor queryExecutor, IMapper autoMapper, ModelMapper modelMapper, SqlCommands sqlCommands, HttpClientService httpClientService, IHttpContextAccessor httpContextAccessor, ICourseService courseService)
        {
            this._autoMapper = autoMapper;
            this._queryExecutor = queryExecutor;
            this._sqlCommands = sqlCommands;
            this._modelMapper = modelMapper;
            this._httpClientService = httpClientService;
            this._httpContextAccessor = httpContextAccessor;
            this._courseService = courseService;
        }

        public CourseStatisticsResponseDTO Get_Course_Satistics_Student_Uuid(string studentUuid)
        {
            try
            {
                StudentResponseDTO student = this._httpClientService.SendRequest<StudentResponseDTO>(HttpMethod.Get, "http://localhost:40001/api/users/students/" + studentUuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            }
            catch
            {
                throw new EntityNotFoundException("Student with uuid " + studentUuid + " doesn't exist", GeneralConsts.MICROSERVICE_NAME);
            }
            CourseStatistics stat = this._queryExecutor.Execute<CourseStatistics>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_COURSE_STATISTICS_STUDENT_UUID(studentUuid), this._modelMapper.MapToCourseStatistics);
            if (stat == null)
                throw new EntityNotFoundException($"No data for student with uuid: " + studentUuid, GeneralConsts.MICROSERVICE_NAME);
            return this._autoMapper.Map<CourseStatisticsResponseDTO>(stat);
        }

        public CourseStatisticsResponseDTO Get_Course_Statistics_Course_Uuid(string courseUuid)
        {
            CourseStatistics stat = this._queryExecutor.Execute<CourseStatistics>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_COURSE_STATISTICS_COURSE_UUID(courseUuid), this._modelMapper.MapToCourseStatistics);
            if (stat == null)
                throw new EntityNotFoundException($"No data for course with uuid: " + courseUuid, GeneralConsts.MICROSERVICE_NAME);
            return this._autoMapper.Map<CourseStatisticsResponseDTO>(stat);
        }

        public CourseStatisticsResponseDTO Get_Course_Statistics_Year(int year)
        {
            CourseStatistics stat = this._queryExecutor.Execute<CourseStatistics>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_COURSE_STATISTCS_YEAR(year), this._modelMapper.MapToCourseStatistics);
            if (stat == null)
                throw new EntityNotFoundException($"No data for year: " + year, GeneralConsts.MICROSERVICE_NAME);
            return this._autoMapper.Map<CourseStatisticsResponseDTO>(stat);
        }
    }
}
