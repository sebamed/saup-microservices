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
    public class DepartmentService : IDepartmentService {

        private readonly QueryExecutor _queryExecutor;
        private readonly ModelMapper _modelMapper;
        private readonly SqlCommands _sqlCommands;
        private readonly IMapper _autoMapper;

        public DepartmentService(QueryExecutor queryExecutor, ModelMapper modelMapper, SqlCommands sqlCommands, IMapper autoMapper) {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._sqlCommands = sqlCommands;
            this._autoMapper = autoMapper;
        }

        //GET ALL
        public List<Department> FindAll() {
            return this._queryExecutor.Execute<List<Department>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_DEPARTMENTS(), this._modelMapper.MapToDepartments);
        }
        public List<DepartmentResponseDTO> GetAll() {
            return this._autoMapper.Map<List<DepartmentResponseDTO>>(this.FindAll());
        }

        //GET BY
        public List<Department> FindByName(string name) {
            return this._queryExecutor.Execute<List<Department>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_DEPARTMENTS_BY_NAME(name), this._modelMapper.MapToDepartments);
        }
        public List<DepartmentResponseDTO> GetByName(string name) {
            return this._autoMapper.Map<List<DepartmentResponseDTO>>(this.FindByName(name));
        }
        public Department FindOneByUUID(string uuid) {
            return this._queryExecutor.Execute<Department>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_DEPARTMENT_BY_UUID(uuid), this._modelMapper.MapToDepartment);
        }
        public DepartmentResponseDTO GetOneByUuid(string uuid) {
            return this._autoMapper.Map<DepartmentResponseDTO>(this.FindOneByUUID(uuid));
        }

        public DepartmentResponseDTO Create(CreateDepartmentRequestDTO requestDTO) {
            if (this.FindByName(requestDTO.name) != null)
                throw new EntityAlreadyExistsException($"Department with name {requestDTO.name} already exists!", GeneralConsts.MICROSERVICE_NAME);

            Department department = new Department() {
                uuid = requestDTO.uuid,
                name = requestDTO.name
            };

            department = this._queryExecutor.Execute<Department>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_DEPARTMENT(department), this._modelMapper.MapToDepartment);

            return this._autoMapper.Map<DepartmentResponseDTO>(department);
        }

        public DepartmentResponseDTO Update(UpdateDepartmentRequestDTO requestDTO) {
            if (this.FindOneByUUID(requestDTO.uuid) == null)
                throw new EntityNotFoundException($"Department with uuid {requestDTO.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
            
            Department department = new Department() {
                uuid = requestDTO.uuid,
                name = requestDTO.name
            };

            department = this._queryExecutor.Execute<Department>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_DEPARTMENT(department), this._modelMapper.MapToDepartment);

            return this._autoMapper.Map<DepartmentResponseDTO>(department);
        }

        public DepartmentResponseDTO Delete(string uuid) {
            if (this.FindOneByUUID(uuid) == null)
                throw new EntityNotFoundException($"Department with uuid {uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            Department old = this.FindOneByUUID(uuid);
            this._queryExecutor.Execute<Department>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_DEPARTMENT(uuid), this._modelMapper.MapToDepartment);
            
            return this._autoMapper.Map<DepartmentResponseDTO>(old);
        }
    }
}
