using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Domain;

namespace UserMicroservice.Mappers {
    public class ModelMapper {

        public User MapToUser(IDataReader reader) {
            while (reader.Read()) {
                return new User() {
                    uuid = Convert.ToString(reader["uuid"]),
                    id = Convert.ToInt32(reader["id"]),
                    name = Convert.ToString(reader["name"]),
                    surname = Convert.ToString(reader["surname"]),
                    password = Convert.ToString(reader["password"]),
                    email = Convert.ToString(reader["email"]),
                    phone = Convert.ToString(reader["phone"]),
                    role = new Role() {
                        uuid = Convert.ToString(reader["role_uuid"]),
                        id = Convert.ToInt32(reader["roleID"]),
                        name = Convert.ToString(reader["role_name"]),
                    }
                };
            }

            return null;
        }

        public List<User> MapToUsers(IDataReader reader) {
            List<User> users = new List<User>();

            while(reader.Read()) {
                users.Add(new User() {
                    uuid = Convert.ToString(reader["uuid"]),
                    id = Convert.ToInt32(reader["id"]),
                    name = Convert.ToString(reader["name"]),
                    surname = Convert.ToString(reader["surname"]),
                    password = Convert.ToString(reader["password"]),
                    email = Convert.ToString(reader["email"]),
                    phone = Convert.ToString(reader["phone"]),
                    role = new Role() {
                        uuid = Convert.ToString(reader["role_uuid"]),
                        id = Convert.ToInt32(reader["roleID"]),
                        name = Convert.ToString(reader["role_name"]),
                    }
                });
            }

            return users;
        }
    }
}
