using AutoMapper;
using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using System.Collections.Generic;
using UserMicroservice.Consts;
using UserMicroservice.Domain;
using UserMicroservice.DTO.Admin.Request;
using UserMicroservice.DTO.Admin.Response;
using UserMicroservice.DTO.User;
using UserMicroservice.Mappers;

namespace UserMicroservice.Services.Implementation {
    public class AdminService : IAdminService {

        private readonly IUserService _userService;

        private readonly QueryExecutor _queryExecutor;

        private readonly ModelMapper _modelMapper;

        private readonly IMapper _autoMapper;

        private readonly SqlCommands _sqlCommands;

        public AdminService(IUserService userService, QueryExecutor queryExecutor, ModelMapper modelMapper, IMapper autoMapper, SqlCommands sqlCommands) {
            this._userService = userService;
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._autoMapper = autoMapper;
            this._sqlCommands = sqlCommands;
        }

        public AdminResponseDTO Create(CreateAdminRequestDTO requestDTO) {

            User user = this._userService.Create(new CreateUserRequestDTO() {
                email = requestDTO.email,
                phone = requestDTO.phone,
                name = requestDTO.name,
                surname = requestDTO.surname,
                password = requestDTO.password,
                roleName = RoleConsts.ROLE_ADMIN
            });

            Admin admin = new Admin() {
                id = user.id,
                uuid = user.uuid,
                email = user.email,
                phone = user.phone,
                name = user.name,
                surname = user.surname
            };

            this._queryExecutor.Execute<Admin>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_ADMIN(admin), this._modelMapper.EmptyMapper<Admin>);

            return this._autoMapper.Map<AdminResponseDTO>(admin);

        }

        public List<AdminResponseDTO> GetAll() {
            return this._autoMapper.Map<List<AdminResponseDTO>>(this.FindAll());
        }

        public AdminResponseDTO GetOneByUuid(string uuid) {
            return this._autoMapper.Map<AdminResponseDTO>(this.FindOneByUuidOrThrow(uuid));
        }

        public Admin FindOneByUuidOrThrow(string uuid) {
            Admin admin = this._queryExecutor.Execute<Admin>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_ADMIN_BY_UUID(uuid), this._modelMapper.MapToAdmin);

            if (admin == null) {
                throw new EntityNotFoundException($"User with uuid: {uuid} does not exist or is not Admin!", GeneralConsts.MICROSERVICE_NAME);
            }

            return admin;
        }

        public List<Admin> FindAll() {
            return this._queryExecutor.Execute<List<Admin>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_ADMINS(), this._modelMapper.MapToAdmins);
        }

    }
}
