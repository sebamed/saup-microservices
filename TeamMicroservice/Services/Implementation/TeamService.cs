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

        public Team FindByName(string name)
        {
            return this._queryExecutor.Execute<Team>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_TEAMS_BY_NAME(name), this._modelMapper.MapToTeam);
        }
        public TeamResponseDTO GetByName(string name)
        {
            TeamResponseDTO response = this._autoMapper.Map<TeamResponseDTO>(this.FindByName(name));
            response.teacher = this._httpClientService.SendRequest<TeacherDTO>(HttpMethod.Get, "http://localhost:40001/api/users/teachers/" + response.teacher.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
        }
        
        public Team FindOneByUUID(string uuid)
        {
            return this._queryExecutor.Execute<Team>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_TEAM_BY_UUID(uuid), this._modelMapper.MapToTeam);
        }
        public TeamResponseDTO GetOneByUuid(string uuid)
        {
            TeamResponseDTO response = this._autoMapper.Map<TeamResponseDTO>(this.FindOneByUUID(uuid));
            response.teacher = this._httpClientService.SendRequest<TeacherDTO>(HttpMethod.Get, "http://localhost:40001/api/users/teachers/" + response.teacher.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
        }

        public TeamResponseDTO Create(CreateTeamRequestDTO requestDTO)
        {
            if (this.FindByName(requestDTO.name) != null)
                throw new EntityAlreadyExistsException($"Team with name {requestDTO.name} already exists!", GeneralConsts.MICROSERVICE_NAME);

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
            response.teacher = this._httpClientService.SendRequest<TeacherDTO>(HttpMethod.Get, "http://localhost:40001/api/users/teachers/" + response.teacher.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
        }

        public TeamResponseDTO Update(UpdateTeamRequestDTO requestDTO)
        {
            if (this.FindOneByUUID(requestDTO.uuid) == null)
                throw new EntityNotFoundException($"Team with uuid {requestDTO.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
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
            response.teacher = this._httpClientService.SendRequest<TeacherDTO>(HttpMethod.Get, "http://localhost:40001/api/users/teachers/" + response.teacher.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
        }

        public TeamResponseDTO Delete(string uuid)
        {
            if (this.FindOneByUUID(uuid) == null)
                throw new EntityNotFoundException($"Team with uuid {uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            Team old = this.FindOneByUUID(uuid);
            this._queryExecutor.Execute<Team>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_TEAM(uuid), this._modelMapper.MapToTeam);

            TeamResponseDTO response = this._autoMapper.Map<TeamResponseDTO>(old);
            response.teacher = this._httpClientService.SendRequest<TeacherDTO>(HttpMethod.Get, "http://localhost:40001/api/users/teachers/" + response.teacher.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
        }
    }
}
