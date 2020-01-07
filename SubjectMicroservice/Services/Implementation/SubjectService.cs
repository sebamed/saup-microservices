using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using System.Collections.Generic;
using SubjectMicroservice.Consts;
using SubjectMicroservice.Domain;
using SubjectMicroservice.DTO.Subject;
using SubjectMicroservice.DTO.Subject.Request;
using SubjectMicroservice.DTO.Subject.Response;
using SubjectMicroservice.Mappers;
using AutoMapper;

namespace SubjectMicroservice.Services.Implementation
{
	public class SubjectService : ISubjectService
	{
		private readonly QueryExecutor _queryExecutor;
		private readonly ModelMapper _modelMapper;
		private readonly SqlCommands _sqlCommands;
		private readonly IMapper _autoMapper;

		public SubjectService(QueryExecutor queryExecutor, ModelMapper modelMapper, SqlCommands sqlCommands, IMapper autoMapper)
		{
			this._queryExecutor = queryExecutor;
			this._modelMapper = modelMapper;
			this._sqlCommands = sqlCommands;
			this._autoMapper = autoMapper;
		}

		public List<Subject> FindAll()
		{
			return this._queryExecutor.Execute<List<Subject>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_SUBJECTS(), this._modelMapper.MapToSubjects);
		}
		public List<SubjectResponseDTO> GetAll()
		{
			return this._autoMapper.Map<List<SubjectResponseDTO>>(this.FindAll());
		}
		public List<Subject> FindByName(string name)
		{
			return this._queryExecutor.Execute<List<Subject>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_SUBJECTS_BY_NAME(name), this._modelMapper.MapToSubjects);
		}
		public List<SubjectResponseDTO> GetByName(string name)
		{
			return this._autoMapper.Map<List<SubjectResponseDTO>>(this.FindByName(name));
		}
		public Subject FindOneByUUID(string uuid)
		{
			return this._queryExecutor.Execute<Subject>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_SUBJECT_BY_UUID(uuid), this._modelMapper.MapToSubject);
		}
		public SubjectResponseDTO GetOneByUuid(string uuid)
		{
			return this._autoMapper.Map<SubjectResponseDTO>(this.FindOneByUUID(uuid));
		}

		public SubjectResponseDTO Create(CreateSubjectRequestDTO requestDTO)
		{
			if (this.FindByName(requestDTO.name) != null)
				throw new EntityAlreadyExistsException($"Subject with name {requestDTO.name} already exists!", GeneralConsts.MICROSERVICE_NAME);

			Subject subject = new Subject()
			{
				name = requestDTO.name,
				description = requestDTO.description,
				creationDate = requestDTO.creationDate
			};

			subject = this._queryExecutor.Execute<Subject>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_SUBJECT(subject), this._modelMapper.MapToSubject);

			return this._autoMapper.Map<SubjectResponseDTO>(subject);
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
				creationDate = requestDTO.creationDate
			};


			subject = this._queryExecutor.Execute<Subject>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_SUBJECT(subject), this._modelMapper.MapToSubject);

			return this._autoMapper.Map<SubjectResponseDTO>(subject);
		}

		public SubjectResponseDTO Delete(string uuid)
		{
			if (this.FindOneByUUID(uuid) == null)
				throw new EntityNotFoundException($"Subject with uuid {uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

			Subject old = this.FindOneByUUID(uuid);
			this._queryExecutor.Execute<Subject>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_SUBJECT(uuid), this._modelMapper.MapToSubject);

			return this._autoMapper.Map<SubjectResponseDTO>(old);
		}
	}
}

