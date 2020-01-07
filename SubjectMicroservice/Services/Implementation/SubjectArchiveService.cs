using AutoMapper;
using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using SubjectMicroservice.Consts;
using SubjectMicroservice.Domain;
using SubjectMicroservice.DTO.SubjectArchive.Request;
using SubjectMicroservice.DTO.SubjectArchive.Response;
using SubjectMicroservice.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectMicroservice.Services.Implementation
{
	public class SubjectArchiveService : ISubjectArchiveService
	{

        private readonly QueryExecutor _queryExecutor;
        private readonly ModelMapper _modelMapper;
        private readonly SqlCommands _sqlCommands;
        private readonly IMapper _autoMapper;

        public SubjectArchiveService(QueryExecutor queryExecutor, ModelMapper modelMapper, SqlCommands sqlCommands, IMapper autoMapper)
        {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._sqlCommands = sqlCommands;
            this._autoMapper = autoMapper;
        }

        //GET ALL
        public List<SubjectArchive> FindAll()
        {
            return this._queryExecutor.Execute<List<SubjectArchive>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_SUBJECT_ARCHIVES(), this._modelMapper.MapToSubjectArchives);
        }
        public List<SubjectArchiveResponseDTO> GetAll()
        {
            return this._autoMapper.Map<List<SubjectArchiveResponseDTO>>(this.FindAll());
        }

        //GET BY
        public List<SubjectArchive> FindByName(string name)
        {
            return this._queryExecutor.Execute<List<SubjectArchive>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_SUBJECT_ARHIVES_BY_NAME(name), this._modelMapper.MapToSubjectArchives);
        }
        public List<SubjectArchiveResponseDTO> GetByName(string name)
        {
            return this._autoMapper.Map<List<SubjectArchiveResponseDTO>>(this.FindByName(name));
        }
        public SubjectArchive FindOneByUUID(string subjectUUID)
        {
            return this._queryExecutor.Execute<SubjectArchive>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_SUBJECT_ARHIVE_BY_UUID(subjectUUID), this._modelMapper.MapToSubjectArchive);
        }
        public SubjectArchiveResponseDTO GetOneByUuid(string subjectUUID)
        {
            return this._autoMapper.Map<SubjectArchiveResponseDTO>(this.FindOneByUUID(subjectUUID));
        }

        public SubjectArchiveResponseDTO Create(CreateSubjectArchiveRequestDTO requestDTO)
        {
            if (this.FindByName(requestDTO.name) != null)
                throw new EntityAlreadyExistsException($"Subject Archive with name {requestDTO.name} already exists!", GeneralConsts.MICROSERVICE_NAME);

            SubjectArchive subjectArchive = new SubjectArchive()
            {
                subjectUUID = requestDTO.subjectUUID,
                name = requestDTO.name,
                description = requestDTO.description,
                creationDate = requestDTO.creationDate,
                changeDate = requestDTO.changeDate,
                version = requestDTO.version
            };

            subjectArchive = this._queryExecutor.Execute<SubjectArchive>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_SUBJECT_ARHIVE(subjectArchive), this._modelMapper.MapToSubjectArchive);

            return this._autoMapper.Map<SubjectArchiveResponseDTO>(subjectArchive);
        }

        public SubjectArchiveResponseDTO Update(UpdateSubjectArchiveRequestDTO requestDTO)
        {
            if (this.FindOneByUUID(requestDTO.subjectUUID) == null)
                throw new EntityNotFoundException($"Subject Archive with subjectUUID {requestDTO.subjectUUID} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            SubjectArchive subjectArchive = new SubjectArchive()
            {
                subjectUUID = requestDTO.subjectUUID,
                name = requestDTO.name,
                description = requestDTO.description,
                changeDate = requestDTO.changeDate,
                version = requestDTO.version
            };

            subjectArchive = this._queryExecutor.Execute<SubjectArchive>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_SUBJECT_ARHIVE(subjectArchive), this._modelMapper.MapToSubjectArchive);

            return this._autoMapper.Map<SubjectArchiveResponseDTO>(subjectArchive);
        }

        public SubjectArchiveResponseDTO Delete(string subjectUUID)
        {
            if (this.FindOneByUUID(subjectUUID) == null)
                throw new EntityNotFoundException($"Subject Archive with subjectUUID {subjectUUID} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            SubjectArchive old = this.FindOneByUUID(subjectUUID);
            this._queryExecutor.Execute<SubjectArchive>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_SUBJECT_ARHIVE(subjectUUID), this._modelMapper.MapToSubjectArchive);

            return this._autoMapper.Map<SubjectArchiveResponseDTO>(old);
        }
    }
}
