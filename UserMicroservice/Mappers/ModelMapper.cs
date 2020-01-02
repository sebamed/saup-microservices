﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Domain;

namespace UserMicroservice.Mappers {
    public class ModelMapper {
        public T EmptyMapper<T>(IDataReader reader) {
            return default(T);
        }

        public Role MapToRole(IDataReader reader) {
            while (reader.Read()) {
                return new Role() {
                    uuid = Convert.ToString(reader["uuid"]),
                    id = Convert.ToInt32(reader["id"]),
                    name = Convert.ToString(reader["name"])
                };
            }

            return null;
        }

        public Admin MapToAdmin(IDataReader reader) {
            while (reader.Read()) {
                return new Admin() {
                    uuid = Convert.ToString(reader["uuid"]),
                    id = Convert.ToInt32(reader["id"]),
                    name = Convert.ToString(reader["name"]),
                    surname = Convert.ToString(reader["surname"]),
                    password = Convert.ToString(reader["password"]),
                    email = Convert.ToString(reader["email"]),
                    phone = Convert.ToString(reader["phone"])
                };
            }

            return null;
        }

        public List<Admin> MapToAdmins(IDataReader reader) {
            List<Admin> admins = new List<Admin>();

            while (reader.Read()) {
                admins.Add(new Admin() {
                    uuid = Convert.ToString(reader["uuid"]),
                    id = Convert.ToInt32(reader["id"]),
                    name = Convert.ToString(reader["name"]),
                    surname = Convert.ToString(reader["surname"]),
                    password = Convert.ToString(reader["password"]),
                    email = Convert.ToString(reader["email"]),
                    phone = Convert.ToString(reader["phone"])
                });
            }

            return admins;
        }

        public User MapToUserAfterInsert(IDataReader reader) {
            reader.Read();
            return new User() {
                uuid = Convert.ToString(reader["uuid"]),
                id = Convert.ToInt32(reader["id"]),
                name = Convert.ToString(reader["name"]),
                surname = Convert.ToString(reader["surname"]),
                password = Convert.ToString(reader["password"]),
                email = Convert.ToString(reader["email"]),
                phone = Convert.ToString(reader["phone"]),
                role = new Role() {
                    id = Convert.ToInt32(reader["roleID"])
                }
            };
        }

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

            while (reader.Read()) {
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