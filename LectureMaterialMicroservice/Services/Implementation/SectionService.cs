using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using System.Collections.Generic;
using LectureMaterialMicroservice.Consts;
using LectureMaterialMicroservice.Domain;
using LectureMaterialMicroservice.DTO.User;
using LectureMaterialMicroservice.Mappers;
using AutoMapper;
using System;
using SectionMicroservice.DTO.Section.Request;
using SectionMicroservice.Domain.External;
using SectionMicroservice.Services;
using SectionMicroservice.DTO.SectionArchive.Request;
using Commons.Domain;
using Microsoft.AspNetCore.Http;

namespace LectureMaterialMicroservice.Services.Implementation {
    public class SectionService : ISectionService {

        private readonly QueryExecutor _queryExecutor;
        private readonly ModelMapper _modelMapper;
        private readonly IMapper _autoMapper;
        private readonly SqlCommands _sqlCommands;
        private readonly ISectionArchiveService _archiveService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SectionService(QueryExecutor queryExecutor, ModelMapper modelMapper, IMapper autoMapper, SqlCommands sqlCommands, ISectionArchiveService archiveService, IHttpContextAccessor httpContextAccessor) {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._autoMapper = autoMapper;
            this._sqlCommands = sqlCommands;
            this._archiveService = archiveService;
            this._httpContextAccessor = httpContextAccessor;
        }

        public List<MultipleSectionResponseDTO> GetAll() {
            return this._autoMapper.Map<List<MultipleSectionResponseDTO>>(this.FindAll());
        }
        public List<Section> FindAll() {
            return this._queryExecutor.Execute<List<Section>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_SECTIONS(), this._modelMapper.MapToSections);
        }
        
        public Section FindOneByUuidOrThrow(string uuid)
        {
            Section section = this._queryExecutor.Execute<Section>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_SECTION_BY_UUID(uuid), this._modelMapper.MapToSection);

            if (section == null)
            {
                throw new EntityNotFoundException($"Section with uuid: {uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }

            return section;
        }
        public SectionResponseDTO GetOneByUuid(string uuid)
        {
            return this._autoMapper.Map<SectionResponseDTO>(this.FindOneByUuidOrThrow(uuid));
        }

        public List<Section> FindAllVisible()  {
            return this._queryExecutor.Execute<List<Section>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_VISIBLE_SECTIONS(), this._modelMapper.MapToSections);
        }
        public List<MultipleSectionResponseDTO> GetVisibleSections()
        {
            return this._autoMapper.Map<List<MultipleSectionResponseDTO>>(this.FindAllVisible());
        }

        public SectionResponseDTO Create(CreateSectionRequestDTO requestDTO)
        {
            Section section = new Section()
            {
                name = requestDTO.name,
                description = requestDTO.description,
                visible = requestDTO.visible,
                creationDate = requestDTO.creationDate,
                course = new Course()
                {
                    uuid = requestDTO.courseUUID
                }
            };

            CreateSectionArchiveRequestDTO archive = new CreateSectionArchiveRequestDTO()
            {
                sectionUUID = section.uuid,
                name = section.name,
                description = section.description,
                visible = section.visible,
                creationDate = section.creationDate,
                courseUUID = section.course.uuid,
                moderatorUUID = new UserPrincipal(_httpContextAccessor.HttpContext).uuid,
                changeDate = DateTime.Now
            };

            section = this._queryExecutor.Execute<Section>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_SECTION(section), this._modelMapper.MapToSection);
            _ = this._archiveService.Create(archive);
            return this._autoMapper.Map<SectionResponseDTO>(section);
        }

        public SectionResponseDTO Update(UpdateSectionRequestDTO requestDTO)
        {
            if (this.FindOneByUuidOrThrow(requestDTO.uuid) == null)
            {
                throw new EntityNotFoundException($"Section with uuid: {requestDTO.uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }
            Section section = new Section()
            {
                uuid = requestDTO.uuid,
                name = requestDTO.name,
                description = requestDTO.description,
                visible = requestDTO.visible,
                creationDate = requestDTO.creationDate,
                course = new Course()
                {
                    uuid = requestDTO.courseUUID
                }
            };

            CreateSectionArchiveRequestDTO archive = new CreateSectionArchiveRequestDTO()
            {
                sectionUUID = section.uuid,
                name = section.name,
                description = section.description,
                visible = section.visible,
                creationDate = section.creationDate,
                courseUUID = section.course.uuid,
                moderatorUUID = new UserPrincipal(_httpContextAccessor.HttpContext).uuid,
                changeDate = DateTime.Now
            };

            section = this._queryExecutor.Execute<Section>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_SECTION(section), this._modelMapper.MapToSection);
            _ = this._archiveService.Create(archive);
            return this._autoMapper.Map<SectionResponseDTO>(section);

        }
        
        public SectionResponseDTO DeleteSection(string uuid) {
            if (this.FindOneByUuidOrThrow(uuid) == null) {
                throw new EntityNotFoundException($"Section with uuid: {uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }

            this._archiveService.Delete(uuid);

            Section old = this.FindOneByUuidOrThrow(uuid);
            this._queryExecutor.Execute<Section>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_SECTION(uuid), this._modelMapper.MapToSection);
            return this._autoMapper.Map<SectionResponseDTO>(old);
        }
    }
}
