using UserMicroservice.Domain;

namespace UserMicroservice.Consts {
    public class SqlCommands {

        public string UPDATE_USER(User user) {
            return $"update SAUP_USER.Users set name = '{ user.name }', surname = '{ user.surname}', email = '{ user.email}', phone = '{ user.phone}' " +
                $"output inserted.*, r.name as 'role_name' from SAUP_USER.Users u join SAUP_USER.Role r on u.roleUUID = r.uuid " +
                $"where u.uuid = '{ user.uuid}';";
        }

        // TODO: opravi ovo da radi jer outputu se nesto ne svidja
        public string CREATE_USER(User user) {
            //return $"insert into SAUP_USER.Users (uuid, name, surname, password, email, phone, roleUUID) output inserted.*, r.name as 'role_name' from SAUP_USER.Users u join SAUP_USER.Role r on u.roleUUID = r.uuid " +
            //    $"values ('{user.uuid}', '{user.name}', '{user.surname}', '{user.password}', '{user.email}', '{user.phone}', '{user.role.uuid}');";
            return $"insert into SAUP_USER.Users (uuid, name, surname, password, email, phone, roleUUID) output inserted.* " +
                $"values ('{user.uuid}', '{user.name}', '{user.surname}', '{user.password}', '{user.email}', '{user.phone}', '{user.role.uuid}');";
        }

        public string CREATE_ADMIN(Admin admin) {
            return $"insert into SAUP_USER.Admin (userUUID) output inserted.* values ('{admin.uuid}');";
        }

        public string CREATE_ADMIN(Teacher teacher) {
            return $"insert into SAUP_USER.Teacher (userUUID) output inserted.* values ('{teacher.uuid}');";
        }

        public string CREATE_STUDENT(Student student) {
            return $"insert into SAUP_USER.Student (userUUID, indexNumber, departmentUUID) output inserted.* values ('{student.uuid}', '{student.indexNumber}', '{student.departmentUuid}');";
        }

        public string GET_ONE_USER_BY_UUID(string uuid) {
            return $"select u.*, r.name as 'role_name', r.uuid as 'role_uuid' " +
                $"from SAUP_USER.Users u " +
                $"join SAUP_USER.Role r on u.roleUUID = r.uuid " +
                $"where u.uuid = '{uuid}'; ";
        }

        public string GET_ALL_USERS() {
            return $"select u.*, r.name as 'role_name', r.uuid as 'role_uuid' " +
                $"from SAUP_USER.Users u " +
                $"join SAUP_USER.Role r on u.roleUUID = r.uuid; ";
        }

        public string GET_ONE_USER_BY_EMAIL(string email) {
            return $"select u.*, r.name as 'role_name', r.uuid as 'role_uuid' " +
               $"from SAUP_USER.Users u " +
               $"join SAUP_USER.Role r on u.roleUUID = r.uuid " +
               $"where u.email = '{email}'; ";
        }

        public string GET_ONE_ADMIN_BY_UUID(string uuid) {
            return $"select u.* from SAUP_USER.Admin a " +
                $"inner join SAUP_USER.Users u on a.userUUID = u.uuid and u.uuid = '{uuid}'";
        }

        public string GET_ONE_TEACHER_BY_UUID(string uuid) {
            return $"select u.* from SAUP_USER.Teacher t " +
                $"inner join SAUP_USER.Users u on t.userUUID = u.uuid and u.uuid = '{uuid}'";
        }

        public string GET_ONE_STUDENT_BY_UUID(string uuid) {
            return $"select u.*, s.* from SAUP_USER.Student s " +
                $"inner join SAUP_USER.Users u on s.userUUID = u.uuid and u.uuid = '{uuid}'";
        }

        public string GET_ONE_STUDENT_BY_INDEX_NUMBER(string indexNumber) {
            return $"select u.*, s.* from SAUP_USER.Student s " +
                $"inner join SAUP_USER.Users u on s.userUUID = u.uuid and s.indexNumber = '{indexNumber}'";
        }
        public string GET_ALL_ADMINS() {
            return "select u.* from SAUP_USER.Admin a " +
                "inner join SAUP_USER.Users u on a.userUUID = u.uuid;";
        }

        public string GET_ALL_TEACHERS() {
            return "select u.* from SAUP_USER.Teacher t " +
                "inner join SAUP_USER.Users u on t.userUUID = u.uuid;";
        }

        public string GET_ALL_STUDENTS() {
            return "select u.*, s.* from SAUP_USER.Student s " +
                "inner join SAUP_USER.Users u on s.userUUID = u.uuid;";
        }


        public string GET_ONE_ROLE_BY_NAME(string name) {
            return $"select * from SAUP_USER.Role where name = '{name}'";
        }
    }
}
