using AutoMapper;
using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.Domain;
using Commons.ExceptionHandling.Exceptions;
using Commons.HttpClientRequests;
using LectureMaterialMicroservice.Consts;
using LectureMaterialMicroservice.Mappers;
using Microsoft.AspNetCore.Http;
using SectionMicroservice.Domain;
using SectionMicroservice.Domain.External;
using SectionMicroservice.DTO.External;
using SectionMicroservice.DTO.SectionArchive.Request;
using SectionMicroservice.DTO.SectionArchive.Response;
using System.Collections.Generic;
using System.Net.Http;
 
namespace SectionMicroservice.Services.Implementation
{
    public class SectionArchiveService : ISectionArchiveService {

        private readonly QueryExecutor _queryExecutor;
        private readonly ModelMapper _modelMapper;
        private readonly IMapper _autoMapper;
        private readonly SqlCommands _sqlCommands;
        private readonly HttpClientService _httpClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SectionArchiveService(QueryExecutor queryExecutor, ModelMapper modelMapper, IMapper autoMapper, SqlCommands sqlCommands, HttpClientService httpClientService, IHttpContextAccessor httpContextAccessor)
        {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._autoMapper = autoMapper;
            this._sqlCommands = sqlCommands;
            this._httpClientService = httpClientService;
            this._httpContextAccessor = httpContextAccessor;
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
            if (response == null)
                return response;
            
            UserDTO moderator = this._httpClientService.SendRequest<UserDTO>(HttpMethod.Get, "http://localhost:40001/api/users/" + response.moderator.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            if (moderator == null)
                throw new EntityAlreadyExistsException($"Moderator with uuid {response.moderator.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
            response.moderator = moderator;

            CourseDTO course = this._httpClientService.SendRequest<CourseDTO>(HttpMethod.Get, "http://localhost:40005/api/courses/" + response.course.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            if (course == null)
                throw new EntityAlreadyExistsException($"Course with uuid {response.course.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
            response.course = course;

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
