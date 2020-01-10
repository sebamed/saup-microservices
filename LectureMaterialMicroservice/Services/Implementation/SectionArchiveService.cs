using AutoMapper;
using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using LectureMaterialMicroservice.Consts;
using LectureMaterialMicroservice.Mappers;
using LectureMaterialMicroservice.Services;
using SectionMicroservice.Domain;
using SectionMicroservice.Domain.External;
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

        public List<MultipleSectionArchiveResponseDTO> GetAllArchivesBySectionUUID(string sectionUUID) {
            return this._autoMapper.Map<List<MultipleSectionArchiveResponseDTO>>(this.FindAllArchivesBySectionUUID(sectionUUID));
        }
        public List<SectionArchive> FindAllArchivesBySectionUUID(string sectionUUID) {
            return this._queryExecutor.Execute<List<SectionArchive>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_ARCHIVES_BY_SECTION_UUID(sectionUUID), this._modelMapper.MapToSectionArchives);
        }

        public SectionArchiveResponseDTO GetLatestVersionBySectionoUUID(string sectionUUID)
        {
            SectionArchiveResponseDTO response = this._autoMapper.Map<SectionArchiveResponseDTO>(this.FindLatestVersionBySectionUUID(sectionUUID));
            return response;
        }
        public SectionArchive FindLatestVersionBySectionUUID(string sectionUUID)
        {
            return this._queryExecutor.Execute<SectionArchive>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_LATEST_ARCHIVE_BY_SECTION_UUID(sectionUUID), this._modelMapper.MapToSectionArchive);
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
                course = new Course()
                {
                    uuid = requestDTO.courseUUID
                },
                moderator = new User()
                {
                    uuid = requestDTO.moderatorUUID
                },
                changeDate = requestDTO.changeDate
            };

            sectionArchive = this._queryExecutor.Execute<SectionArchive>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_ARCHIVE(sectionArchive), this._modelMapper.MapToSectionArchive);

            return this._autoMapper.Map<SectionArchiveResponseDTO>(sectionArchive);
        }

        public void Delete(string sectionUUID)
        {
            _ = this._queryExecutor.Execute<List<SectionArchive>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_ARCHIVES_BY_SECTION_UUID(sectionUUID), this._modelMapper.MapToSectionArchives);
        }
    }
}
