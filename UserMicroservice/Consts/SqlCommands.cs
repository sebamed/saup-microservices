using UserMicroservice.Domain;

namespace UserMicroservice.Consts {
    public class SqlCommands {

        public string CREATE_USER(User user) {
            return $"insert into SAUP_USER.Users (uuid, name, surname, password, email, phone, roleID) output inserted.* " +
                $"values ('{user.uuid}', '{user.name}', '{user.surname}', '{user.password}', '{user.email}', '{user.phone}', {user.role.id});";
        }

        public string CREATE_ADMIN(Admin admin) {
            return $"insert into SAUP_USER.Admin (userID) output inserted.* values ({admin.id});";
        }

        public string GET_ONE_USER_BY_UUID(string uuid) {
            return $"select u.*, r.name as 'role_name', r.uuid as 'role_uuid' " +
                $"from SAUP_USER.Users u " +
                $"join SAUP_USER.Role r on u.roleID = r.id " +
                $"where u.uuid = '{uuid}'; ";
        }

        public string GET_ALL_USERS() {
            return $"select u.*, r.name as 'role_name', r.uuid as 'role_uuid' " +
                $"from SAUP_USER.Users u " +
                $"join SAUP_USER.Role r on u.roleID = r.id; ";
        }

        public string GET_ONE_USER_BY_EMAIL(string email) {
            return $"select u.*, r.name as 'role_name', r.uuid as 'role_uuid' " +
               $"from SAUP_USER.Users u " +
               $"join SAUP_USER.Role r on u.roleID = r.id " +
               $"where u.email = '{email}'; ";
        }

        public string GET_ONE_ADMIN_BY_UUID(string uuid) {
            return $"select u.* from SAUP_USER.Admin a " +
                $"inner join SAUP_USER.Users u on a.userID = u.id and u.uuid = '{uuid}'";
        }

        public string GET_ALL_ADMINS() {
            return "select u.* from SAUP_USER.Admin a " +
                "inner join SAUP_USER.Users u on a.userID = u.id;";
        }

        public string GET_ONE_ROLE_BY_NAME(string name) {
            return $"select * from SAUP_USER.Role where name = '{name}'";
        }
    }
}
