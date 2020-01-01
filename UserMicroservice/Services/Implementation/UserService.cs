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

        private readonly QueryExecutor _queryExecutor;

        private readonly ModelMapper _modelMapper;

        private readonly IMapper _autoMapper;

        private readonly SqlCommands _sqlCommands;

        public UserService(QueryExecutor queryExecutor, ModelMapper modelMapper, IMapper autoMapper, SqlCommands sqlCommands) {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._autoMapper = autoMapper;
            this._sqlCommands = sqlCommands;
        }

        public UserResponseDTO Create(CreateUserRequestDTO requestDTO) {
            // todo
            return new UserResponseDTO();
        }

        public List<UserResponseDTO> GetAll() {
            // todo
            return this._autoMapper.Map<List<UserResponseDTO>>(this.FindAll());
        }
        public UserResponseDTO GetOneByUuid(string uuid) {                  
            return this._autoMapper.Map<UserResponseDTO>(this.FindOneByUuidOrThrow(uuid));
        }

        public UserResponseDTO Update(UpdateUserRequestDTO requestDTO) {
            // todo
            return new UserResponseDTO();
        }

        public User FindOneByUuidOrThrow(string uuid) {
            User user = this._queryExecutor.Execute<User>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_USERS_WITH_ROLE(uuid), this._modelMapper.MapToUser);

            if (user == null) {
                throw new EntityNotFoundException($"User with uuid: {uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }

            return user;
        }

        public List<User> FindAll() {
            return this._queryExecutor.Execute<List<User>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL(), this._modelMapper.MapToUsers);
        }
    }

}
