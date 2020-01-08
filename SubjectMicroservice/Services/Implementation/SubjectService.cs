using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using System.Collections.Generic;
using SubjectMicroservice.Consts;
using SubjectMicroservice.Domain;
using SubjectMicroservice.DTO.Subject.Request;
using SubjectMicroservice.DTO.Subject.Response;
using SubjectMicroservice.Mappers;
using AutoMapper;
using Commons.HttpClientRequests;
using SubjectMicroservice.DTO.Department;
using System.Net.Http;
using Commons.Domain;
using Microsoft.AspNetCore.Http;
using SubjectMicroservice.DTO.External;
using SubjectMicroservice.Domain.External;

namespace SubjectMicroservice.Services.Implementation
{
	public class SubjectService : ISubjectService
	{
		private readonly QueryExecutor _queryExecutor;
		private readonly ModelMapper _modelMapper;
		private readonly SqlCommands _sqlCommands;
		private readonly IMapper _autoMapper;
        private readonly HttpClientService _httpClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubjectService(QueryExecutor queryExecutor, ModelMapper modelMapper, SqlCommands sqlCommands, IMapper autoMapper, HttpClientService httpClientService, IHttpContextAccessor httpContextAccessor)
		{
			this._queryExecutor = queryExecutor;
			this._modelMapper = modelMapper;
			this._sqlCommands = sqlCommands;
			this._autoMapper = autoMapper;
            this._httpClientService = httpClientService;
            this._httpContextAccessor = httpContextAccessor;
        }

		public List<Subject> FindAll()
		{
			return this._queryExecutor.Execute<List<Subject>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_SUBJECTS(), this._modelMapper.MapToSubjects);
		}
		public List<MultipleSubjectResponseDTO> GetAll()
		{
			return this._autoMapper.Map<List<MultipleSubjectResponseDTO>>(this.FindAll());
		}
		public List<Subject> FindByName(string name)
		{
			return this._queryExecutor.Execute<List<Subject>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_SUBJECTS_BY_NAME(name), this._modelMapper.MapToSubjects);
		}
		public List<MultipleSubjectResponseDTO> GetByName(string name)
		{
			return this._autoMapper.Map<List<MultipleSubjectResponseDTO>>(this.FindByName(name));
		}

        public List<Subject> FindByDepartmentUUID(string departmentUUID) {
            return this._queryExecutor.Execute<List<Subject>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_SUBJECTS_BY_DEPARTMENT_UUID(departmentUUID), this._modelMapper.MapToSubjects);
        }
        public List<MultipleSubjectResponseDTO> GetByDepartmentUUID(string uuid) {
            return this._autoMapper.Map<List<MultipleSubjectResponseDTO>>(this.FindByDepartmentUUID(uuid));
        }

        public List<Subject> FindByCreatorUUID(string creatorUUID)
        {
            return this._queryExecutor.Execute<List<Subject>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_SUBJECTS_BY_CREATOR_UUID(creatorUUID), this._modelMapper.MapToSubjects);
        }
        public List<MultipleSubjectResponseDTO> GetByCreatorUUID(string uuid)
        {
            return this._autoMapper.Map<List<MultipleSubjectResponseDTO>>(this.FindByCreatorUUID(uuid));
        }
        public Subject FindOneByUUID(string uuid)
		{
			return this._queryExecutor.Execute<Subject>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_SUBJECT_BY_UUID(uuid), this._modelMapper.MapToSubject);
		}
		public SubjectResponseDTO GetOneByUuid(string uuid)
		{
            SubjectResponseDTO response = this._autoMapper.Map<SubjectResponseDTO>(this.FindOneByUUID(uuid));
            response.department = this._httpClientService.SendRequest<DepartmentDTO>(HttpMethod.Get, "http://localhost:40007/api/departments/" + response.department.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            response.creator = this._httpClientService.SendRequest<UserDTO>(HttpMethod.Get, "http://localhost:40001/api/users/" + response.creator.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
        }

		public SubjectResponseDTO Create(CreateSubjectRequestDTO requestDTO)
		{
            if (this.FindByName(requestDTO.name) != null)
                throw new EntityAlreadyExistsException($"Subject with name {requestDTO.name} already exists!", GeneralConsts.MICROSERVICE_NAME);

            Subject subject = new Subject()
            {
                name = requestDTO.name,
                description = requestDTO.description,
                creationDate = requestDTO.creationDate,
                department = new Department() {
                    uuid = requestDTO.departmentUUID
                },
                creator = new User() {
                    uuid = requestDTO.creatorUUID
                }
            };

			subject = this._queryExecutor.Execute<Subject>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_SUBJECT(subject), this._modelMapper.MapToSubject);
            SubjectResponseDTO response = this._autoMapper.Map<SubjectResponseDTO>(subject);
            response.department = this._httpClientService.SendRequest<DepartmentDTO>(HttpMethod.Get, "http://localhost:40007/api/departments/" + response.department.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            response.creator = this._httpClientService.SendRequest<UserDTO>(HttpMethod.Get, "http://localhost:40001/api/users/" + response.creator.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
		}

		public SubjectResponseDTO Update(UpdateSubjectRequestDTO requestDTO)
		{
			if (this.FindOneByUUID(requestDTO.uuid) == null)
				throw new EntityNotFoundException($"Subject with uuid {requestDTO.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

			Subject subject = new Subject()
			{
				uuid = requestDTO.uuid,
				name = requestDTO.name,
				description = requestDTO.description,
				creationDate = requestDTO.creationDate,
                department = new Department()
                {
                    uuid = requestDTO.departmentUUID
                },
                creator = new User()
                {
                    uuid = requestDTO.creatorUUID
                }
            };


			subject = this._queryExecutor.Execute<Subject>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_SUBJECT(subject), this._modelMapper.MapToSubject);

            SubjectResponseDTO response = this._autoMapper.Map<SubjectResponseDTO>(subject);
            response.department = this._httpClientService.SendRequest<DepartmentDTO>(HttpMethod.Get, "http://localhost:40007/api/departments/" + response.department.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            response.creator = this._httpClientService.SendRequest<UserDTO>(HttpMethod.Get, "http://localhost:40001/api/users/" + response.creator.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
        }

		public SubjectResponseDTO Delete(string uuid)
		{
			if (this.FindOneByUUID(uuid) == null)
				throw new EntityNotFoundException($"Subject with uuid {uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

			Subject old = this.FindOneByUUID(uuid);
			this._queryExecutor.Execute<Subject>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_SUBJECT(uuid), this._modelMapper.MapToSubject);

            SubjectResponseDTO response = this._autoMapper.Map<SubjectResponseDTO>(old);
            response.department = this._httpClientService.SendRequest<DepartmentDTO>(HttpMethod.Get, "http://localhost:40007/api/departments/" + response.department.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            response.creator = this._httpClientService.SendRequest<UserDTO>(HttpMethod.Get, "http://localhost:40001/api/users/" + response.creator.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            return response;
        }
    }
}

