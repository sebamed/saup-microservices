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

        public Faculty MapToFacultyAfterInsert(IDataReader reader) {
            reader.Read();
            return new Faculty() {
                id = Convert.ToInt32(reader["id"]),
                uuid = Convert.ToString(reader["uuid"]),
                name = Convert.ToString(reader["name"]),
                city = Convert.ToString(reader["city"]),
                phone = Convert.ToString(reader["phone"])
            };
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
					name = Convert.ToString(reader["name"])
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
					name = Convert.ToString(reader["name"])
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
				name = Convert.ToString(reader["name"])
			};
		}
	}
}
