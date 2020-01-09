using AutoMapper;
using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using LectureMaterialMicroservice.Consts;
using LectureMaterialMicroservice.Mappers;
using LectureMaterialMicroservice.Services;
using SectionMicroservice.Domain;
using SectionMicroservice.DTO.SectionArchive.Request;
using SectionMicroservice.DTO.SectionArchive.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.Services.Implementation
{
    public class SectionArchiveService : ISectionArchiveService {

        private readonly QueryExecutor _queryExecutor;
        private readonly ModelMapper _modelMapper;
        private readonly IMapper _autoMapper;
        private readonly SqlCommands _sqlCommands;

        public SectionArchiveService(QueryExecutor queryExecutor, ModelMapper modelMapper, IMapper autoMapper, SqlCommands sqlCommands)
        {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._autoMapper = autoMapper;
            this._sqlCommands = sqlCommands;
        }

        public SectionArchiveResponseDTO Create(CreateSectionArchiveRequestDTO requestDTO)
        {
            SectionArchive sectionArchive = new SectionArchive()
            {
                sectionUUID = requestDTO.sectionUUID,
                name = requestDTO.name,
                description = requestDTO.description,
                visible = requestDTO.visible,
                creationDate = requestDTO.creationDate,
                courseUUID = requestDTO.courseUUID,
                moderatorUUID = requestDTO.moderatorUUID,
                changeDate = requestDTO.changeDate
            };

            sectionArchive = this._queryExecutor.Execute<SectionArchive>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_ARCHIVE(sectionArchive), this._modelMapper.MapToSectionArchive);

            return this._autoMapper.Map<SectionArchiveResponseDTO>(sectionArchive);
        }

        public SectionArchive GetOneByArchiveBySectionUuid(string sectionUUID)
        {

            SectionArchive sectionArchive = this._queryExecutor.Execute<SectionArchive>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_ARCHIVE_BY_SECTION_UUID(sectionUUID), this._modelMapper.MapToSectionArchive);

            if (sectionArchive == null)
            {
                throw new EntityNotFoundException($"Archive with sectionUUID: {sectionUUID} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }

            return sectionArchive;
        }

    }
}
