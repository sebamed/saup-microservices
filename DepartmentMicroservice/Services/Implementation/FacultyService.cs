using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using System.Collections.Generic;
using DepartmentMicroservice.Consts;
using DepartmentMicroservice.Domain;
using DepartmentMicroservice.DTO.User;
using DepartmentMicroservice.DTO.User.Request;
using DepartmentMicroservice.Mappers;
using AutoMapper;

namespace DepartmentMicroservice.Services.Implementation {
    public class FacultyService : IFacultyService {

        private readonly QueryExecutor _queryExecutor;
        private readonly ModelMapper _modelMapper;
        private readonly SqlCommands _sqlCommands;
        private readonly IMapper _autoMapper;

        public FacultyService(QueryExecutor queryExecutor, ModelMapper modelMapper, SqlCommands sqlCommands, IMapper autoMapper) {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._sqlCommands = sqlCommands;
            this._autoMapper = autoMapper;
        }

        public FacultyResponseDTO Create(CreateFacultyRequestDTO requestDTO) {
            // todo
            return new FacultyResponseDTO();
        }
        public List<Faculty> FindAll() {
            return this._queryExecutor.Execute<List<Faculty>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_FACULTIES(), this._modelMapper.MapToFaculties);
        }
        public List<FacultyResponseDTO> GetAll() {
            return this._autoMapper.Map<List<FacultyResponseDTO>>(this.FindAll());
        }
        public FacultyResponseDTO GetOneByUuid(string uuid) {
            // todo
            return new FacultyResponseDTO();
        }

        public FacultyResponseDTO Update(UpdateFacultyRequestDTO requestDTO) {
            // todo
            return new FacultyResponseDTO();
        }       
    }

}
