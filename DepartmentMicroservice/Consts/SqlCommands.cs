using DepartmentMicroservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentMicroservice.Consts {
    public class SqlCommands {
        public string GET_ALL_FACULTIES() {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Faculty";
        }
        public string GET_FACULTY_BY_UUID(string uuid) {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Faculty where uuid = '{uuid}'";
        }
        public string GET_FACULTIES_BY_CITY(string city) {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Faculty where city = '{city}'";
        }
        public string GET_FACULTIES_BY_NAME(string name) {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Faculty where name = '{name}'";
        }

        public string CREATE_FACULTY(Faculty faculty){
            return $"insert into {GeneralConsts.SCHEMA_NAME}.Faculty (uuid, name, city, phone) output inserted.* " +
                $"values ('{faculty.uuid}', '{faculty.name}', '{faculty.city}', '{faculty.phone}');";
        }

        public string UPDATE_FACULTY(Faculty faculty) {
            return $"update {GeneralConsts.SCHEMA_NAME}.Faculty " +
                $"set name = '{faculty.name}', city = '{faculty.city}', phone = '{faculty.phone}' output inserted.* " +
                $"where uuid = '{faculty.uuid}';";
        }

        public string DELETE_FACULTY(string uuid) {
            return $"delete from {GeneralConsts.SCHEMA_NAME}.Faculty " +
                $"where uuid = '{uuid}';";
        }
    }
}
