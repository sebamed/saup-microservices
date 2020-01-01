using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Consts {
    public class SqlCommands {

        public string GET_ALL_USERS_WITH_ROLE(string uuid) {
            return $"select u.*, r.name as 'role_name', r.uuid as 'role_uuid' " +
                $"from SAUP_USER.Users u " +
                $"join SAUP_USER.Role r on u.roleID = r.id " +
                $"where u.uuid = '{uuid}'; ";
        }

        public string GET_ALL() {
            return $"select u.*, r.name as 'role_name', r.uuid as 'role_uuid' " +
                $"from SAUP_USER.Users u " +
                $"join SAUP_USER.Role r on u.roleID = r.id; ";
        }

    }
}
