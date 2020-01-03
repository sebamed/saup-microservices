using AutoMapper;
using Commons.Consts;
using Commons.DatabaseUtils;
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
            throw new NotImplementedException();
        }

        public List<SectionArchiveResponseDTO> GetAll()
        {
            return this._autoMapper.Map<List<SectionArchiveResponseDTO>>(this.FindAll());
        }

        public List<SectionArchive> FindAll()
        {
            return this._queryExecutor.Execute<List<SectionArchive>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_ARCHIVES(), this._modelMapper.MapToSectionArchives);
        }
        public SectionArchiveResponseDTO GetOneByUuid(string uuid)
        {
            throw new NotImplementedException();
        }

        public List<SectionArchiveResponseDTO> GetVisibleSections()
        {
            throw new NotImplementedException();
        }
    }
}
