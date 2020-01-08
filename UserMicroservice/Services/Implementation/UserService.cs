using AutoMapper;
using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using System.Collections.Generic;
using UserMicroservice.Consts;
using UserMicroservice.Domain;
using UserMicroservice.DTO.User;
using UserMicroservice.DTO.User.Request;
using UserMicroservice.Mappers;

namespace UserMicroservice.Services.Implementation {
    public class UserService : IUserService {

        private readonly IRoleService _roleService;

        private readonly QueryExecutor _queryExecutor;

        private readonly ModelMapper _modelMapper;

        private readonly IMapper _autoMapper;

        private readonly SqlCommands _sqlCommands;

        public UserService(IRoleService roleService, QueryExecutor queryExecutor, ModelMapper modelMapper, IMapper autoMapper, SqlCommands sqlCommands) {
            this._roleService = roleService;
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._autoMapper = autoMapper;
            this._sqlCommands = sqlCommands;
        }

        public User Create(CreateUserRequestDTO requestDTO) {
            // Checking if the user with provided email already exists
            this.ThrowExceptionIfEmailExists(requestDTO.email);

            User user = new User() {
                email = requestDTO.email,
                phone = requestDTO.phone,
                name = requestDTO.name,
                surname = requestDTO.surname,
                password = BCrypt.Net.BCrypt.HashPassword(requestDTO.password),
                role = this._roleService.FindOneByName(requestDTO.roleName)
            };

            user = this._queryExecutor.Execute<User>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_USER(user), this._modelMapper.MapToUserAfterInsert);

            return user;
        }

        public List<UserResponseDTO> GetAll() {
            return this._autoMapper.Map<List<UserResponseDTO>>(this.FindAll());
        }
        public UserResponseDTO GetOneByUuid(string uuid) {
            return this._autoMapper.Map<UserResponseDTO>(this.FindOneByUuidOrThrow(uuid));
        }

        public UserResponseDTO Update(UpdateUserRequestDTO requestDTO) {
            this.ThrowExceptionIfEmailExists(requestDTO.email);

            User user = this.FindOneByUuidOrThrow(requestDTO.uuid);
            user = this._autoMapper.Map<User>(requestDTO);

            user = this._queryExecutor.Execute<User>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_USER(user), this._modelMapper.MapToUserAfterUpdate);

            return this._autoMapper.Map<UserResponseDTO>(user);
        }

        public User FindOneByUuidOrThrow(string uuid) {
            User user = this._queryExecutor.Execute<User>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_USER_BY_UUID(uuid), this._modelMapper.MapToUser);

            if (user == null) {
                throw new EntityNotFoundException($"User with uuid: {uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }

            return user;
        }

        public User FindOneByEmailAddress(string email) {
            return this._queryExecutor.Execute<User>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_USER_BY_EMAIL(email), this._modelMapper.MapToUser);
        }

        public List<User> FindAll() {
            return this._queryExecutor.Execute<List<User>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_USERS(), this._modelMapper.MapToUsers);
        }

        private void ThrowExceptionIfEmailExists(string email) {
            if (this.FindOneByEmailAddress(email) != null) {
                throw new EntityAlreadyExistsException($"User with email {email} already exists!", GeneralConsts.MICROSERVICE_NAME);
            }
        }
    }

}
