using AutoMapper;
using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.Domain;
using Commons.ExceptionHandling.Exceptions;
using Commons.HttpClientRequests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TeamMicroservice.Consts;
using TeamMicroservice.Domain;
using TeamMicroservice.Domain.External;
using TeamMicroservice.DTO.External;
using TeamMicroservice.DTO.Team.Request;
using TeamMicroservice.DTO.Team.Response;
using TeamMicroservice.Mappers;

namespace TeamMicroservice.Services.Implementation
{
    public class TeamService : ITeamService
    {

        private readonly QueryExecutor _queryExecutor;
        private readonly ModelMapper _modelMapper;
        private readonly SqlCommands _sqlCommands;
        private readonly IMapper _autoMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClientService _httpClientService;

        public TeamService(QueryExecutor queryExecutor, ModelMapper modelMapper, SqlCommands sqlCommands, IMapper autoMapper, IHttpContextAccessor httpContextAccessor, HttpClientService httpClientService)
        {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._sqlCommands = sqlCommands;
            this._autoMapper = autoMapper;
            this._httpContextAccessor = httpContextAccessor;
            this._httpClientService = httpClientService;
        }

        public List<Team> FindAll()
        {
            return this._queryExecutor.Execute<List<Team>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_TEAMS(), this._modelMapper.MapToTeams);
        }
        public List<MultipleTeamResponseDTO> GetAll()
        {
            return this._autoMapper.Map<List<MultipleTeamResponseDTO>>(this.FindAll());
        }

        public List<Team> FindTeamsByCourse(string courseUUID)
        {
            return this._queryExecutor.Execute<List<Team>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_TEAMS_BY_COURSE(courseUUID), this._modelMapper.MapToTeams);
        }
        public List<MultipleTeamResponseDTO> GetTeamsByCourse(string courseUUID)
        {
            return this._autoMapper.Map<List<MultipleTeamResponseDTO>>(this.FindTeamsByCourse(courseUUID));
        }

        public List<Team> FindByName(string name)
        {
            return this._queryExecutor.Execute<List<Team>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_TEAM_BY_NAME(name), this._modelMapper.MapToTeams);
        }
        public List<MultipleTeamResponseDTO> GetByName(string name)
        {
            return this._autoMapper.Map<List<MultipleTeamResponseDTO>>(this.FindByName(name));
        }
        
        public Team FindOneByUUID(string uuid)
        {
            return this._queryExecutor.Execute<Team>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_TEAM_BY_UUID(uuid), this._modelMapper.MapToTeam);
        }
        public TeamResponseDTO GetOneByUuid(string uuid)
        {
            TeamResponseDTO response = this._autoMapper.Map<TeamResponseDTO>(this.FindOneByUUID(uuid));
            if (response == null)
                throw new EntityAlreadyExistsException($"Team with uuid {uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
            
            TeacherDTO teacher = this._httpClientService.SendRequest<TeacherDTO>(HttpMethod.Get, "http://localhost:40001/api/users/teachers/" + response.teacher.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            if (teacher == null)
                throw new EntityAlreadyExistsException($"Teacher with uuid {response.teacher.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
            response.teacher = teacher;

            CourseDTO course = this._httpClientService.SendRequest<CourseDTO>(HttpMethod.Get, "http://localhost:40005/api/courses/" + response.course.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            if (course == null)
                throw new EntityAlreadyExistsException($"Course with uuid {response.course.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
            response.course = course;

            return response;
        }

        public Team FindOneByNameAndCourse(string name, string courseUUID)
        {
            return this._queryExecutor.Execute<Team>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_TEAM_BY_NAME_AND_COURSE(name,courseUUID), this._modelMapper.MapToTeam);
        }

        public TeamResponseDTO Create(CreateTeamRequestDTO requestDTO)
        {
            if (this.FindOneByNameAndCourse(requestDTO.name,requestDTO.courseUUID) != null)
                throw new EntityAlreadyExistsException($"Team with name {requestDTO.name} already exists in Course {requestDTO.courseUUID}!", GeneralConsts.MICROSERVICE_NAME);

            TeacherDTO teacher = this._httpClientService.SendRequest<TeacherDTO>(HttpMethod.Get, "http://localhost:40001/api/users/teachers/" + requestDTO.teacherUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            if(teacher == null)
                throw new EntityAlreadyExistsException($"Teacher with uuid {requestDTO.teacherUUID} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            CourseDTO course = this._httpClientService.SendRequest<CourseDTO>(HttpMethod.Get, "http://localhost:40005/api/courses/" + requestDTO.courseUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            if (course == null)
                throw new EntityAlreadyExistsException($"Course with uuid {requestDTO.courseUUID} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            Team team = new Team()
            {
                name = requestDTO.name,
                description = requestDTO.description,
                teacher = new Teacher()
                {
                    uuid = requestDTO.teacherUUID
                },
                course = new Course()
                {
                    uuid = requestDTO.courseUUID
                }
            };

            team = this._queryExecutor.Execute<Team>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_TEAM(team), this._modelMapper.MapToTeam);
            
            TeamResponseDTO response = this._autoMapper.Map<TeamResponseDTO>(team);
            response.teacher = teacher;
            response.course = course;

            return response;
        }

        public TeamResponseDTO Update(UpdateTeamRequestDTO requestDTO)
        {
            var old = this.FindOneByUUID(requestDTO.uuid);
            if (old == null)
                throw new EntityNotFoundException($"Team with uuid {requestDTO.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            var similar = this.FindOneByNameAndCourse(requestDTO.name, requestDTO.courseUUID);
            if (similar != null && similar.name != old.name)
                throw new EntityAlreadyExistsException($"Team with name {requestDTO.name} already exists in Course {requestDTO.courseUUID}!", GeneralConsts.MICROSERVICE_NAME);

            TeacherDTO teacher = this._httpClientService.SendRequest<TeacherDTO>(HttpMethod.Get, "http://localhost:40001/api/users/teachers/" + requestDTO.teacherUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            if (teacher == null)
                throw new EntityAlreadyExistsException($"Teacher with uuid {requestDTO.teacherUUID} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            CourseDTO course = this._httpClientService.SendRequest<CourseDTO>(HttpMethod.Get, "http://localhost:40005/api/courses/" + requestDTO.courseUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            if (course == null)
                throw new EntityAlreadyExistsException($"Course with uuid {requestDTO.courseUUID} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            Team team = new Team()
            {
                uuid = requestDTO.uuid,
                name = requestDTO.name,
                description = requestDTO.description,
                teacher = new Teacher()
                {
                    uuid = requestDTO.teacherUUID
                },
                course = new Course()
                {
                    uuid = requestDTO.courseUUID
                }
            };

            team = this._queryExecutor.Execute<Team>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_TEAM(team), this._modelMapper.MapToTeam);

            TeamResponseDTO response = this._autoMapper.Map<TeamResponseDTO>(team);
            response.teacher = teacher;
            response.course = course;
            return response;
        }

        public TeamResponseDTO Delete(string uuid)
        {
            if (this.FindOneByUUID(uuid) == null)
                throw new EntityNotFoundException($"Team with uuid {uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            TeamResponseDTO response = this.GetOneByUuid(uuid);
            this._queryExecutor.Execute<Team>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_TEAM(uuid), this._modelMapper.MapToTeam);

            return response;
        }
    }
}
