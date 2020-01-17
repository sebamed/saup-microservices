using AutoMapper;
using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.Domain;
using Commons.ExceptionHandling.Exceptions;
using Commons.HttpClientRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserMicroservice.Consts;
using UserMicroservice.Domain;
using UserMicroservice.DTO.Teacher.Request;
using UserMicroservice.DTO.Teacher.Response;
using UserMicroservice.DTO.User;
using UserMicroservice.Mappers;

namespace UserMicroservice.Services.Implementation {
    public class TeacherService : ITeacherService {

        private readonly IUserService _userService;

        private readonly QueryExecutor _queryExecutor;

        private readonly ModelMapper _modelMapper;

        private readonly IMapper _autoMapper;

        private readonly SqlCommands _sqlCommands;


        public TeacherService(IUserService userService, QueryExecutor queryExecutor, ModelMapper modelMapper, IMapper autoMapper, SqlCommands sqlCommands) {
            this._userService = userService;
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._autoMapper = autoMapper;
            this._sqlCommands = sqlCommands;
        }

        public TeacherResponseDTO Create(CreateTeacherRequestDTO requestDTO) {
            User user = this._userService.Create(new CreateUserRequestDTO() {
                email = requestDTO.email,
                phone = requestDTO.phone,
                name = requestDTO.name,
                surname = requestDTO.surname,
                password = requestDTO.password,
                roleName = RoleConsts.ROLE_TEACHER_ONLY
            });

            Teacher teacher = new Teacher() {
                id = user.id,
                uuid = user.uuid,
                email = user.email,
                phone = user.phone,
                name = user.name,
                surname = user.surname
            };

            this._queryExecutor.Execute<Teacher>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_ADMIN(teacher), this._modelMapper.EmptyMapper<Teacher>);

            return this._autoMapper.Map<TeacherResponseDTO>(teacher);
        }

        public List<TeacherResponseDTO> GetAll() {
            return this._autoMapper.Map<List<TeacherResponseDTO>>(this.FindAll());
        }

        public TeacherResponseDTO GetOneByUuid(string uuid) {
            return this._autoMapper.Map<TeacherResponseDTO>(this.FindOneByUuidOrThrow(uuid));
        }

        public Teacher FindOneByUuidOrThrow(string uuid) {
            Teacher teacher = this._queryExecutor.Execute<Teacher>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_TEACHER_BY_UUID(uuid), this._modelMapper.MapToTeacher);

            if (teacher == null) {
                throw new EntityNotFoundException($"User with uuid: {uuid} does not exist or is not Teacher!", GeneralConsts.MICROSERVICE_NAME);
            }

            return teacher;
        }

        public List<Teacher> FindAll() {
            return this._queryExecutor.Execute<List<Teacher>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_TEACHERS(), this._modelMapper.MapToTeachers);
        }

    }
}
