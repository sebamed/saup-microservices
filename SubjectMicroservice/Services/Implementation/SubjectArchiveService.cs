using AutoMapper;
using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.Domain;
using Commons.ExceptionHandling.Exceptions;
using Commons.HttpClientRequests;
using Microsoft.AspNetCore.Http;
using SubjectMicroservice.Consts;
using SubjectMicroservice.Domain;
using SubjectMicroservice.Domain.External;
using SubjectMicroservice.DTO.Department;
using SubjectMicroservice.DTO.External;
using SubjectMicroservice.DTO.SubjectArchive.Request;
using SubjectMicroservice.DTO.SubjectArchive.Response;
using SubjectMicroservice.Mappers;
using System.Collections.Generic;
using System.Net.Http;

namespace SubjectMicroservice.Services.Implementation
{
	public class SubjectArchiveService : ISubjectArchiveService
	{

        private readonly QueryExecutor _queryExecutor;
        private readonly ModelMapper _modelMapper;
        private readonly SqlCommands _sqlCommands;
        private readonly IMapper _autoMapper;
        private readonly HttpClientService _httpClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubjectArchiveService(QueryExecutor queryExecutor, ModelMapper modelMapper, SqlCommands sqlCommands, IMapper autoMapper, HttpClientService httpClientService, IHttpContextAccessor httpContextAccessor)
        {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._sqlCommands = sqlCommands;
            this._autoMapper = autoMapper;
            this._httpClientService = httpClientService;
            this._httpContextAccessor = httpContextAccessor;
        }

        //GET
        public List<SubjectArchive> FindAllArchivesBySubjectUUID(string subjectUUID) {
            return this._queryExecutor.Execute<List<SubjectArchive>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_ARCHIVES_BY_SUBJECT_UUID(subjectUUID), this._modelMapper.MapToSubjectArchives);
        }
        public List<MultipleSubjectArchiveResponseDTO> GetAllArchivesBySubjectUUID(string subjectUUID) {
            return this._autoMapper.Map<List<MultipleSubjectArchiveResponseDTO>>(this.FindAllArchivesBySubjectUUID(subjectUUID));
        }

        public SubjectArchive FindLatestVersionBySubjectUUID(string subjectUUID) {
            return this._queryExecutor.Execute<SubjectArchive>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_LATEST_ARCHIVE_BY_SUBJECT_UUID(subjectUUID), this._modelMapper.MapToSubjectArchive);
        }
        public SubjectArchiveResponseDTO GetLatestVersionBySubjectUUID(string subjectUUID) {
            SubjectArchiveResponseDTO response = this._autoMapper.Map<SubjectArchiveResponseDTO>(this.FindLatestVersionBySubjectUUID(subjectUUID));
            if (response == null)
                throw new EntityAlreadyExistsException($"Subject archive with uuid {subjectUUID} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            try {
                response.department = this._httpClientService.SendRequest<DepartmentDTO>(HttpMethod.Get, "http://localhost:40007/api/departments/" + response.department.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            } catch {
                throw new EntityNotFoundException($"Department with uuid {response.department.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
            }

            try {
                response.creator = this._httpClientService.SendRequest<UserDTO>(HttpMethod.Get, "http://localhost:40001/api/users/" + response.creator.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            } catch {
                throw new EntityNotFoundException($"User with uuid {response.creator.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
            }

            try {
                response.moderator = this._httpClientService.SendRequest<UserDTO>(HttpMethod.Get, "http://localhost:40001/api/users/" + response.moderator.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            } catch {
                throw new EntityNotFoundException($"User with uuid {response.creator.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
            }
            return response;
        }
         
        //CREATE
        public SubjectArchiveResponseDTO Create(CreateSubjectArchiveRequestDTO requestDTO) {
            SubjectArchive subjectArchive = new SubjectArchive() {
                subjectUUID = requestDTO.subjectUUID,
                name = requestDTO.name,
                description = requestDTO.description,
                creationDate = requestDTO.creationDate,
                department = new Department() {
                    uuid = requestDTO.departmentUUID
                },
                moderator = new User() {
                    uuid = requestDTO.moderatorUUID
                },
                creator = new User() {
                    uuid = requestDTO.creatorUUID
                },
                changeDate = requestDTO.changeDate
            };

            subjectArchive = this._queryExecutor.Execute<SubjectArchive>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_SUBJECT_ARHIVE(subjectArchive), this._modelMapper.MapToSubjectArchive);

            return this._autoMapper.Map<SubjectArchiveResponseDTO>(subjectArchive); ;
        }

        public void Delete(string subjectUUID) {
            _ = this._queryExecutor.Execute<List<SubjectArchive>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_ARCHIVES_BY_SUBJECT_UUID(subjectUUID), this._modelMapper.MapToSubjectArchives);
        }
    }
}
