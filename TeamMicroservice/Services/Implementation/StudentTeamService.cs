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
    public class StudentTeamService : IStudentTeamService
    {

        private readonly QueryExecutor _queryExecutor;
        private readonly ModelMapper _modelMapper;
        private readonly SqlCommands _sqlCommands;
        private readonly IMapper _autoMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClientService _httpClientService;
        private readonly ITeamService _teamService;

        public StudentTeamService(ITeamService teamService, QueryExecutor queryExecutor, ModelMapper modelMapper, SqlCommands sqlCommands, IMapper autoMapper, IHttpContextAccessor httpContextAccessor, HttpClientService httpClientService)
        {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._sqlCommands = sqlCommands;
            this._autoMapper = autoMapper;
            this._httpContextAccessor = httpContextAccessor;
            this._httpClientService = httpClientService;
            this._teamService = teamService;
        }

        public StudentTeamResponseDTO AddStudentIntoTeam(AddStudentIntoTeamDTO requestDTO) {
            if (this._teamService.GetOneByUuid(requestDTO.teamUUID) == null)
                throw new EntityAlreadyExistsException($"Team with uuid {requestDTO.teamUUID} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            StudentTeam studentTeam = new StudentTeam() {
                team = new Team()
                {
                    uuid = requestDTO.teamUUID,
                    course = new Course()
                    {
                        uuid = requestDTO.courseUUID
                    }
                },
                student = new Student()
                {
                    uuid = requestDTO.studentUUID
                }
            };

            studentTeam = this._queryExecutor.Execute<StudentTeam>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.ADD_STUDENT_INTO_TEAM(studentTeam), this._modelMapper.MapToStudentTeam);
            
            StudentTeamResponseDTO response = this._autoMapper.Map<StudentTeamResponseDTO>(studentTeam);
            response.team = this._teamService.GetOneByUuid(requestDTO.teamUUID);
            response.student = this._httpClientService.SendRequest<StudentDTO>(HttpMethod.Get, "http://localhost:40001/api/users/students/" + response.student.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
        }
    }
}
