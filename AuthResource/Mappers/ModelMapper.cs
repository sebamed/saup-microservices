using AuthResource.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AuthResource.Mappers {
    public class ModelMapper {
        
        public AuthenticatedUser MapToAuthenticatedUser(IDataReader reader) {
            while (reader.Read()) {
                return new AuthenticatedUser() {
                    uuid = Convert.ToString(reader["uuid"]),
                    email = Convert.ToString(reader["email"]),
                    role = Convert.ToString(reader["role"])
                };
            }

            return null;
        }

        public EmailAndPassword MapToEmailAndPassword(IDataReader reader) {
            while (reader.Read()) {
                return new EmailAndPassword() {
                    email = Convert.ToString(reader["email"]),
                    password = Convert.ToString(reader["password"])
                };
            }

            return null;
        }
    }
}
