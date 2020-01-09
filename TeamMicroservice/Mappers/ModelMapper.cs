using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TeamMicroservice.Domain;

namespace TeamMicroservice.Mappers {
    public class ModelMapper {

        public List<Team> MapToTeams(IDataReader reader) {
            List<Team> teams = new List<Team>();

            while(reader.Read()) {
                teams.Add(new Team() {
                    id = Convert.ToInt32(reader["id"]),
                    uuid = Convert.ToString(reader["uuid"]),
                    name = Convert.ToString(reader["name"]),
                    description = Convert.ToString(reader["description"])
                });
            }

            return teams;
        }

        public Team MapToTeam(IDataReader reader) {
            while (reader.Read()) {
                return new Team() {
                    id = Convert.ToInt32(reader["id"]),
                    uuid = Convert.ToString(reader["uuid"]),
                    name = Convert.ToString(reader["name"]),
                    description = Convert.ToString(reader["description"])
                };
            }

            return null;
        }

        public Team MapToTeamAfterInsert(IDataReader reader)
        {
            reader.Read();
            return new Team()
            {
                id = Convert.ToInt32(reader["id"]),
                uuid = Convert.ToString(reader["uuid"]),
                name = Convert.ToString(reader["name"]),
                description = Convert.ToString(reader["description"])
            };
        }
    }
}
