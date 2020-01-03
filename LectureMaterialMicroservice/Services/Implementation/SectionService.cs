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

namespace LectureMaterialMicroservice.Services.Implementation {
    public class SectionService : ISectionService {

        private readonly QueryExecutor _queryExecutor;
        private readonly ModelMapper _modelMapper;
        private readonly IMapper _autoMapper;
        private readonly SqlCommands _sqlCommands;

        public SectionService(QueryExecutor queryExecutor, ModelMapper modelMapper, IMapper autoMapper, SqlCommands sqlCommands) {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._autoMapper = autoMapper;
            this._sqlCommands = sqlCommands;
        }

        public List<SectionResponseDTO> GetAll() {
            return this._autoMapper.Map<List<SectionResponseDTO>>(this.FindAll());
        }

        public List<Section> FindAll() {
            return this._queryExecutor.Execute<List<Section>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_SECTIONS(), this._modelMapper.MapToSections);
        }

        public SectionResponseDTO GetOneByUuid(string uuid)
        {
            return this._autoMapper.Map<SectionResponseDTO>(this.FindOneByUuidOrThrow(uuid));
        }

        public SectionResponseDTO Create(CreateSectionRequestDTO requestDTO)
        {
            Section section = new Section()
            {
                name = requestDTO.name,
                description = requestDTO.description,
                visible = requestDTO.visible,
                creationDate = requestDTO.creationDate
            };

            section = this._queryExecutor.Execute<Section>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_SECTION(section), this._modelMapper.MapToSection);

            return this._autoMapper.Map<SectionResponseDTO>(section);
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
        public List<Section> FindAllVisible()  {
            return this._queryExecutor.Execute<List<Section>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_VISIBLE_SECTIONS(), this._modelMapper.MapToSections);
        }

        public List<SectionResponseDTO> GetVisibleSections() {
            return this._autoMapper.Map<List<SectionResponseDTO>>(this.FindAllVisible());
        }

        public SectionResponseDTO DeleteSectionByUUID(string uuid) {
            if (this.FindOneByUuidOrThrow(uuid) == null) {
                throw new EntityNotFoundException($"Section with uuid: {uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }

            Section old = this.FindOneByUuidOrThrow(uuid);
            this._queryExecutor.Execute<Section>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_SECTION_BY_UUID(uuid), this._modelMapper.MapToSection);
            return this._autoMapper.Map<SectionResponseDTO>(old);
        }
    }
}
