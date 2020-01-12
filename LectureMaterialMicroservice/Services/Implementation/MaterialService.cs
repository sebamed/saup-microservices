using AutoMapper;
using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.Domain;
using Commons.ExceptionHandling.Exceptions;
using Commons.HttpClientRequests;
using LectureMaterialMicroservice.Consts;
using LectureMaterialMicroservice.Domain;
using LectureMaterialMicroservice.Mappers;
using LectureMaterialMicroservice.Services;
using Microsoft.AspNetCore.Http;
using SectionMicroservice.Domain;
using SectionMicroservice.Domain.External;
using SectionMicroservice.DTO.External;
using SectionMicroservice.DTO.Material.Request;
using SectionMicroservice.DTO.Material.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SectionMicroservice.Services.Implementation
{
    public class MaterialService : IMaterialService {

        private readonly QueryExecutor _queryExecutor;
        private readonly ModelMapper _modelMapper;
        private readonly IMapper _autoMapper;
        private readonly SqlCommands _sqlCommands;
        private readonly HttpClientService _httpClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISectionService _sectionService;

        public MaterialService(HttpClientService httpClientService, IHttpContextAccessor httpContextAccessor, ISectionService sectionService, QueryExecutor queryExecutor, ModelMapper modelMapper, IMapper autoMapper, SqlCommands sqlCommands)
        {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._autoMapper = autoMapper;
            this._sqlCommands = sqlCommands;
            this._httpClientService = httpClientService;
            this._httpContextAccessor = httpContextAccessor;
            this._sectionService = sectionService;
        }

        public Material FindOneByUuid(string sectionUUID, string fileUUID)
        {
            return this._queryExecutor.Execute<Material>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_FILE_BY_SECTION_AND_FILE_UUID(sectionUUID, fileUUID), this._modelMapper.MapToMaterial);
        }

        public MaterialResponseDTO GetOneByUuidOrThrow(string sectionUUID, string fileUUID) {
            MaterialResponseDTO response = this._autoMapper.Map<MaterialResponseDTO>(this.FindOneByUuid(sectionUUID,fileUUID));
            if (response == null)
                throw new EntityNotFoundException($"Section with uuid: {sectionUUID} and file with uuid: {fileUUID} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            response.file = this._httpClientService.SendRequest<FileDTO>(HttpMethod.Get, "http://localhost:40003/api/files/" + response.file.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            response.section = this._sectionService.GetOneByUuid(sectionUUID);
            return response;
        }

        public MaterialResponseDTO Create(CreateMaterialRequestDTO requestDTO) 
        {
            Material material = new Material()
            {
                section = new Section() {
                  uuid = requestDTO.sectionUUID 
                },
                file = new File()
                {
                    uuid = requestDTO.fileUUID 
                },
                visible = requestDTO.visible
            };

            material = this._queryExecutor.Execute<Material>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.ADD_FILE_TO_SECTION(material), this._modelMapper.MapToMaterial);

            MaterialResponseDTO response = this._autoMapper.Map<MaterialResponseDTO>(material);
            if(response == null)
                throw new EntityNotFoundException($"Section with uuid: {requestDTO.sectionUUID} and file with uuid: {requestDTO.fileUUID} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            response.file = this._httpClientService.SendRequest<FileDTO>(HttpMethod.Get, "http://localhost:40003/api/files/" + response.file.uuid, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
            response.section = this._sectionService.GetOneByUuid(requestDTO.sectionUUID);
            return response;
        }

        public MaterialResponseDTO Delete(string sectionUUID, string fileUUID)
        {
            if (this.FindOneByUuid(sectionUUID, fileUUID) == null)
            {
                throw new EntityNotFoundException($"Section with uuid: {sectionUUID} and file with uuid: {fileUUID} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }

            MaterialResponseDTO response = this.GetOneByUuidOrThrow(sectionUUID, fileUUID);
            _ = this._queryExecutor.Execute<Material>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_FILE_FROM_SECTION(sectionUUID, fileUUID), this._modelMapper.MapToMaterial);
            return response;
        }
    }
}
