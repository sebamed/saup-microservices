using AuthResource.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthResource.Consts {
    public class SqlCommands {

        public string GET_EMAIL_AND_PASSWORD(SignInRequestDTO requestDTO) {
            return $"select email, password from SAUP_USER.Users where email = '{requestDTO.email}'";
        }

        public string GET_AUTHENTICATED_USER(string email) {
            return $"select u.email, u.uuid, r.name as 'role' from SAUP_USER.Users u inner join SAUP_USER.Role r on r.uuid = u.roleUUID where u.email = '{email}';";
        }

    }
}
