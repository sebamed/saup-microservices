using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TeamMicroservice.Domain;
using TeamMicroservice.Domain.External;

namespace TeamMicroservice.Mappers {
    public class ModelMapper {

        public List<Team> MapToTeams(IDataReader reader) {
            List<Team> teams = new List<Team>();

            while(reader.Read()) {
                teams.Add(new Team() {
                    id = Convert.ToInt32(reader["id"]),
                    uuid = Convert.ToString(reader["uuid"]),
                    name = Convert.ToString(reader["name"]),
                    description = Convert.ToString(reader["description"]),
                    teacher = new  Teacher()
                    {
                        uuid = Convert.ToString(reader["teacherUUID"])
                    },
                    course = new Course ()
                    {
                        uuid = Convert.ToString(reader["courseUUID"])
                    }
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
                    description = Convert.ToString(reader["description"]),
                    teacher = new Teacher()
                    {
                        uuid = Convert.ToString(reader["teacherUUID"])
                    },
                    course = new Course()
                    {
                        uuid = Convert.ToString(reader["courseUUID"])
                    }
                };
            }

            return null;
        }

        public StudentTeam MapToStudentTeam(IDataReader reader)
        {
            while (reader.Read())
            {
                return new StudentTeam()
                {
                    team = new Team() {
                        uuid = Convert.ToString(reader["teamUUID"]),
                        course = new Course()
                        {
                            uuid = Convert.ToString(reader["courseUUID"])
                        }
                    },
                    student = new Student()
                    {
                        uuid = Convert.ToString(reader["studentUUID"])
                    }
                };
            }

            return null;
        }

        public List<Student> MapToStudentsInTeam(IDataReader reader)
        {
            List<Student> studentsInTeam = new List<Student>();
            while (reader.Read())
            {
                studentsInTeam.Add( new Student()
                    {
                        uuid = Convert.ToString(reader["studentUUID"])
                    }
                );
            }

            return studentsInTeam;
        }
    }
}
