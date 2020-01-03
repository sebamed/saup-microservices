using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DepartmentMicroservice.Domain;

namespace DepartmentMicroservice.Mappers {
    public class ModelMapper {

        public List<Faculty> MapToFaculties(IDataReader reader) {
            List<Faculty> instruments = new List<Faculty>();

            while(reader.Read()) {
                instruments.Add(new Faculty() {
                    id = Convert.ToInt32(reader["id"]),
                    uuid = Convert.ToString(reader["uuid"]),
                    name = Convert.ToString(reader["name"]),
                    city = Convert.ToString(reader["city"]),
                    phone = Convert.ToString(reader["phone"])
                });
            }

            return instruments;
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
    }
}
