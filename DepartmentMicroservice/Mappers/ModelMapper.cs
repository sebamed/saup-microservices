using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DepartmentMicroservice.Domain;

namespace DepartmentMicroservice.Mappers {
    public class ModelMapper {

        public List<Faculty> MapToFaculties(IDataReader reader) {
            List<Faculty> faculties = new List<Faculty>();

            while(reader.Read()) {
                faculties.Add(new Faculty() {
                    id = Convert.ToInt32(reader["id"]),
                    uuid = Convert.ToString(reader["uuid"]),
                    name = Convert.ToString(reader["name"]),
                    city = Convert.ToString(reader["city"]),
                    phone = Convert.ToString(reader["phone"])
                });
            }

            return faculties;
        }

        public Faculty MapToFaculty(IDataReader reader) {
            while (reader.Read()) {
                return new Faculty() {
                    id = Convert.ToInt32(reader["id"]),
                    uuid = Convert.ToString(reader["uuid"]),
                    name = Convert.ToString(reader["name"]),
                    city = Convert.ToString(reader["city"]),
                    phone = Convert.ToString(reader["phone"])
                };
            }
            return null;
        }

		public List<Department> MapToDepartments(IDataReader reader)
		{
			List<Department> departments = new List<Department>();

			while (reader.Read())
			{
				departments.Add(new Department()
				{
					id = Convert.ToInt32(reader["id"]),
					uuid = Convert.ToString(reader["uuid"]),
					name = Convert.ToString(reader["name"]),
                    faculty = new Faculty() {
                        uuid = Convert.ToString(reader["facultyUUID"]),
                        name = Convert.ToString(reader["fname"]),
                        city = Convert.ToString(reader["fcity"]),
                        phone = Convert.ToString(reader["fphone"]),
                    }
                });
			}

			return departments;
		}

		public Department MapToDepartment(IDataReader reader)
		{
			while (reader.Read())
			{
				return new Department()
				{
					id = Convert.ToInt32(reader["id"]),
					uuid = Convert.ToString(reader["uuid"]),
					name = Convert.ToString(reader["name"]),
                    faculty = new Faculty() {
                        uuid = Convert.ToString(reader["facultyUUID"]),
                        name = Convert.ToString(reader["fname"]),
                        city = Convert.ToString(reader["fcity"]),
                        phone = Convert.ToString(reader["fphone"]),
                    }
                };
			}
			return null;
		}

		public Department MapToDepartmentAfterInsert(IDataReader reader)
		{
			reader.Read();
			return new Department()
			{
				id = Convert.ToInt32(reader["id"]),
				uuid = Convert.ToString(reader["uuid"]),
				name = Convert.ToString(reader["name"]),
                faculty = new Faculty()
                {
                    uuid = Convert.ToString(reader["facultyUUID"])
                }
            };
		}
	}
}
