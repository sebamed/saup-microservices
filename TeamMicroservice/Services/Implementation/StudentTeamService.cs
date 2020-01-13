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

        public StudentTeam FindByStudentAndTeam(string studentUUID, string teamUUID)
        {
            return this._queryExecutor.Execute<StudentTeam>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_BY_TEAM_AND_STUDENT(studentUUID, teamUUID), this._modelMapper.MapToStudentTeam);
        }

        public StudentTeamResponseDTO GetOneByStudentAndTeam(string studentUUID, string teamUUID)
        {
            StudentTeamResponseDTO response = this._autoMapper.Map<StudentTeamResponseDTO>(this.FindByStudentAndTeam(studentUUID,teamUUID));
            if(response == null)
                throw new EntityAlreadyExistsException($"Student with uuid {studentUUID} doesn't exist in Team with uuid {teamUUID}!", GeneralConsts.MICROSERVICE_NAME);
            
            response.team = this._teamService.GetOneByUuid(teamUUID);
            response.student = this._httpClientService.SendRequest<StudentDTO>(HttpMethod.Get, "http://localhost:40001/api/users/students/" + response.student.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
        }

        public StudentTeamResponseDTO AddStudentIntoTeam(AddStudentIntoTeamDTO requestDTO) {
            if (this._teamService.GetOneByUuid(requestDTO.teamUUID) == null)
                throw new EntityAlreadyExistsException($"Team with uuid {requestDTO.teamUUID} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            StudentDTO student;
            try {
                student = this._httpClientService.SendRequest<StudentDTO>(HttpMethod.Get, "http://localhost:40001/api/users/students/" + requestDTO.studentUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            } catch {
                throw new EntityAlreadyExistsException($"Student with uuid {requestDTO.studentUUID} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
            }
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
            response.student = student;
            return response;
        }

        public StudentTeamResponseDTO DeleteStudentFromTeam(string studentUUID, string teamUUID){
            if (this._teamService.GetOneByUuid(teamUUID) == null)
                throw new EntityAlreadyExistsException($"Team with uuid {teamUUID} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            if (this._httpClientService.SendRequest<StudentDTO>(HttpMethod.Get, "http://localhost:40001/api/users/students/" + studentUUID, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result == null)
                throw new EntityAlreadyExistsException($"Student with uuid {studentUUID} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            var response = this.GetOneByStudentAndTeam(studentUUID, teamUUID);

            _ = this._queryExecutor.Execute<StudentTeam>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_STUDENT_FROM_TEAM(studentUUID,teamUUID), this._modelMapper.MapToStudentTeam);

            return response;
        }
    }
}
