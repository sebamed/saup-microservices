using AutoMapper;
using Commons.Consts;
using Commons.DatabaseUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Consts;
using UserMicroservice.Domain;
using UserMicroservice.Mappers;

namespace UserMicroservice.Services.Implementation {
    public class RoleService : IRoleService {

        private readonly QueryExecutor _queryExecutor;

        private readonly ModelMapper _modelMapper;

        private readonly IMapper _autoMapper;

        private readonly SqlCommands _sqlCommands;

        public RoleService(QueryExecutor queryExecutor, ModelMapper modelMapper, IMapper autoMapper, SqlCommands sqlCommands) {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._autoMapper = autoMapper;
            this._sqlCommands = sqlCommands;
        }

        public Role FindOneByName(string name) {
            return this._queryExecutor.Execute<Role>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_ROLE_BY_NAME(name), this._modelMapper.MapToRole);
        }

    }
}
