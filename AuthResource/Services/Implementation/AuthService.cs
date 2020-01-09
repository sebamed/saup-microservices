using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthResource.Consts;
using AuthResource.Domain;
using AuthResource.DTO.User;
using AuthResource.Mappers;
using AuthResource.Utils;
using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;

namespace AuthResource.Services.Implementation {
    public class AuthService : IAuthService {

        private readonly QueryExecutor _queryExecutor;

        private readonly ModelMapper _modelMapper;

        private readonly SqlCommands _sqlCommands;

        private readonly JwtGenerator _jwtGenerator;

        public AuthService(JwtGenerator jwtGenerator, QueryExecutor queryExecutor, ModelMapper modelMapper, SqlCommands sqlCommands) {
            this._jwtGenerator = jwtGenerator;
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._sqlCommands = sqlCommands;
        }

        public SignInResponseDTO SignIn(SignInRequestDTO requestDTO) {
            // todo (get the actual data from db, check the passwords etc.)
            // get user from db
            EmailAndPassword credentials = this._queryExecutor.Execute<EmailAndPassword>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_EMAIL_AND_PASSWORD(requestDTO), this._modelMapper.MapToEmailAndPassword);

            // validate pw with bcrypt
            if (credentials == null || !BCrypt.Net.BCrypt.Verify(requestDTO.password, credentials.password)) {
                throw new BadCredentialsException("Bad credentials!", GeneralConsts.MICROSERVICE_NAME);
            }

            AuthenticatedUser user = this._queryExecutor.Execute<AuthenticatedUser>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_AUTHENTICATED_USER(requestDTO.email), this._modelMapper.MapToAuthenticatedUser);

            // build token and return response
            return new SignInResponseDTO() {
                email = requestDTO.email,
                uuid = user.uuid,
                token = this._jwtGenerator.Generate(user.uuid, user.role)
            };
        }
    }
}
