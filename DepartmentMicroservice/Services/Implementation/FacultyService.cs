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

        //GET ALL
        public List<Faculty> FindAll() {
            return this._queryExecutor.Execute<List<Faculty>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_FACULTIES(), this._modelMapper.MapToFaculties);
        }
        public List<FacultyResponseDTO> GetAll() {
            return this._autoMapper.Map<List<FacultyResponseDTO>>(this.FindAll());
        }

        //GET BY
        public List<Faculty> FindByName(string name) {
            return this._queryExecutor.Execute<List<Faculty>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_FACULTIES_BY_NAME(name), this._modelMapper.MapToFaculties);
        }
        public List<FacultyResponseDTO> GetByName(string name) {
            return this._autoMapper.Map<List<FacultyResponseDTO>>(this.FindByName(name));
        }
        public List<Faculty> FindByCity(string city) {
            return this._queryExecutor.Execute<List<Faculty>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_FACULTIES_BY_CITY(city), this._modelMapper.MapToFaculties);
        }
        public List<FacultyResponseDTO> GetByCity(string city) {
            return this._autoMapper.Map<List<FacultyResponseDTO>>(this.FindByCity(city));
        }
        public Faculty FindOneByUUID(string uuid) {
            return this._queryExecutor.Execute<Faculty>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_FACULTY_BY_UUID(uuid), this._modelMapper.MapToFaculty);
        }
        public FacultyResponseDTO GetOneByUuid(string uuid) {
            var response = this._autoMapper.Map<FacultyResponseDTO>(this.FindOneByUUID(uuid));
            if (response == null)
                throw new EntityNotFoundException($"Faculty with uuid {uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
            return response;
        }

        public Faculty FindOneByNameAndCity(string name, string city) {
            return this._queryExecutor.Execute<Faculty>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_FACULTY_BY_NAME_AND_CITY(name,city), this._modelMapper.MapToFaculty);
        }

        public FacultyResponseDTO Create(CreateFacultyRequestDTO requestDTO) {
            if (this.FindOneByNameAndCity(requestDTO.name,requestDTO.city) != null)
                throw new EntityNotFoundException($"Faculty with name {requestDTO.name} in city {requestDTO.city} already exists!", GeneralConsts.MICROSERVICE_NAME);

            Faculty faculty = new Faculty() {
                name = requestDTO.name,
                phone = requestDTO.phone,
                city = requestDTO.city
            };

            faculty = this._queryExecutor.Execute<Faculty>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_FACULTY(faculty), this._modelMapper.MapToFaculty);

            return this._autoMapper.Map<FacultyResponseDTO>(faculty);
        }

        public FacultyResponseDTO Update(UpdateFacultyRequestDTO requestDTO) {
            var old = this.FindOneByUUID(requestDTO.uuid);
            if (old == null)
                throw new EntityNotFoundException($"Faculty with uuid {requestDTO.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
            
            var similar = this.FindOneByNameAndCity(requestDTO.name, requestDTO.city);
            if (similar != null && similar.name != old.name)
                throw new EntityNotFoundException($"Faculty with name {requestDTO.name} in city {requestDTO.city} already exists!", GeneralConsts.MICROSERVICE_NAME);

            Faculty faculty = new Faculty() {
                uuid = requestDTO.uuid,
                name = requestDTO.name,
                phone = requestDTO.phone,
                city = requestDTO.city
            };

            faculty = this._queryExecutor.Execute<Faculty>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_FACULTY(faculty), this._modelMapper.MapToFaculty);

            return this._autoMapper.Map<FacultyResponseDTO>(faculty);
        }

        public FacultyResponseDTO Delete(string uuid) {
            if (this.FindOneByUUID(uuid) == null)
                throw new EntityNotFoundException($"Faculty with uuid {uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            Faculty old = this.FindOneByUUID(uuid);
            this._queryExecutor.Execute<Faculty>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_FACULTY(uuid), this._modelMapper.MapToFaculty);
            
            return this._autoMapper.Map<FacultyResponseDTO>(old);
        }
    }
}
