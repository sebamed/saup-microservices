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

        public string GET_FACULTY_BY_NAME_AND_CITY(string name, string city)
        {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Faculty where name = '{name}' and city = '{city}'";
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

		public string GET_ALL_DEPARTMENTS()
		{
			return $"select d.*, f.name as fname, f.city as fcity, f.phone as fphone " +
                $"from {GeneralConsts.SCHEMA_NAME}.Department d " +
                $"join {GeneralConsts.SCHEMA_NAME}.Faculty f on d.facultyUUID = f.uuid;";
		}
		public string GET_DEPARTMENT_BY_UUID(string uuid)
		{
            return $"select d.*, f.name as fname, f.city as fcity, f.phone as fphone " +
                $"from {GeneralConsts.SCHEMA_NAME}.Department d " +
                $"join {GeneralConsts.SCHEMA_NAME}.Faculty f on d.facultyUUID = f.uuid " +
                $"where d.uuid = '{uuid}'";
		}
        public string GET_DEPARTMENT_BY_NAME_AND_FACULTY(string name, string facultyUUID)
        {
            return $"select d.*, f.name as fname, f.city as fcity, f.phone as fphone " +
                $"from {GeneralConsts.SCHEMA_NAME}.Department d " +
                $"join {GeneralConsts.SCHEMA_NAME}.Faculty f on d.facultyUUID = f.uuid " +
                $"where d.name = '{name}' and d.facultyUUID = '{facultyUUID}'";
        }

        public string GET_DEPARTMENTS_BY_NAME(string name)
		{
            return $"select d.*, f.name as fname, f.city as fcity, f.phone as fphone " +
                $"from {GeneralConsts.SCHEMA_NAME}.Department d " +
                $"join {GeneralConsts.SCHEMA_NAME}.Faculty f on d.facultyUUID = f.uuid " +
                $"where d.name = '{name}'";
		}
        public string GET_DEPARTMENT_BY_FACULTY_NAME(string facultyName) {
            return $"select d.*, f.name as fname, f.city as fcity, f.phone as fphone " +
                $"from {GeneralConsts.SCHEMA_NAME}.Department d " +
                $"join {GeneralConsts.SCHEMA_NAME}.Faculty f on d.facultyUUID = f.uuid " +
                $"where f.name = '{facultyName}'";
        }


        public string CREATE_DEPARTMENT(Department department)
		{
			return $"insert into {GeneralConsts.SCHEMA_NAME}.Department (uuid, name, facultyUUID) " +
                $"output inserted.* " +
                $"values ('{department.uuid}', '{department.name}', '{department.faculty.uuid}');";
		}

		public string UPDATE_DEPARTMENT(Department department)
		{
			return $"update {GeneralConsts.SCHEMA_NAME}.Department " +
				$"set uuid = '{department.uuid}', name = '{department.name}', facultyUUID = '{department.faculty.uuid}' " +
                $"output inserted.* " +
                $"where uuid = '{department.uuid}';";
		}

		public string DELETE_DEPARTMENT(string uuid)
		{
			return $"delete from {GeneralConsts.SCHEMA_NAME}.Department " +
				$"where uuid = '{uuid}';";
		}
	}
}
