using AutoMapper;
using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Consts;
using UserMicroservice.Domain;
using UserMicroservice.DTO.Student.Request;
using UserMicroservice.DTO.Student.Response;
using UserMicroservice.DTO.User;
using UserMicroservice.Mappers;

namespace UserMicroservice.Services.Implementation {
    public class StudentService : IStudentService {

        private readonly IUserService _userService;

        private readonly QueryExecutor _queryExecutor;

        private readonly ModelMapper _modelMapper;

        private readonly IMapper _autoMapper;

        private readonly SqlCommands _sqlCommands;

        public StudentService(IUserService userService, QueryExecutor queryExecutor, ModelMapper modelMapper, IMapper autoMapper, SqlCommands sqlCommands) {
            this._userService = userService;
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._autoMapper = autoMapper;
            this._sqlCommands = sqlCommands;
        }

        public StudentResponseDTO Create(CreateStudentRequestDTO requestDTO) {
            if (this.FindOneByIndexNumber(requestDTO.indexNumber) != null)
                throw new EntityAlreadyExistsException($"Student with the index number: {requestDTO.indexNumber} already exists!", GeneralConsts.MICROSERVICE_NAME);
            
            User user = this._userService.Create(new CreateUserRequestDTO() {
                email = requestDTO.email,
                phone = requestDTO.phone,
                name = requestDTO.name,
                surname = requestDTO.surname,
                password = requestDTO.password,
                roleName = RoleConsts.ROLE_STUDENT_ONLY
            });

            Student student = new Student() {
                id = user.id,
                uuid = user.uuid,
                email = user.email,
                phone = user.phone,
                name = user.name,
                surname = user.surname,
                departmentUuid = requestDTO.departmentUuid,
                indexNumber = requestDTO.indexNumber
            };

            this._queryExecutor.Execute<Student>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_STUDENT(student), this._modelMapper.EmptyMapper<Student>);

            return this._autoMapper.Map<StudentResponseDTO>(student);
        }

        public List<StudentResponseDTO> GetAll() {
            return this._autoMapper.Map<List<StudentResponseDTO>>(this.FindAll());
        }

        public StudentResponseDTO GetOneByUuid(string uuid) {
            return this._autoMapper.Map<StudentResponseDTO>(this.FindOneByUuidOrThrow(uuid));
        }
        public Student FindOneByUuidOrThrow(string uuid) {
            Student student = this._queryExecutor.Execute<Student>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_STUDENT_BY_UUID(uuid), this._modelMapper.MapToStudent);

            if (student == null) {
                throw new EntityNotFoundException($"User with uuid: {uuid} does not exist or is not Student!", GeneralConsts.MICROSERVICE_NAME);
            }

            return student;
        }

        public List<Student> FindAll() {
            return this._queryExecutor.Execute<List<Student>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_STUDENTS(), this._modelMapper.MapToStudents);
        }

        public Student FindOneByIndexNumber(string indexNumber) {
            return this._queryExecutor.Execute<Student>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_STUDENT_BY_INDEX_NUMBER(indexNumber), this._modelMapper.MapToStudent);
        }

    }
}
