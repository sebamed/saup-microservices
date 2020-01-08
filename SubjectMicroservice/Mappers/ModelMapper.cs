using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SubjectMicroservice.Domain;
using SubjectMicroservice.Domain.External;
using SubjectMicroservice.DTO.Department;

namespace SubjectMicroservice.Mappers {
    public class ModelMapper {

        public List<Subject> MapToSubjects(IDataReader reader) {
            List<Subject> subjects = new List<Subject>();

            while(reader.Read()) {
                subjects.Add(new Subject() {
                    id = Convert.ToInt32(reader["id"]),
                    uuid = Convert.ToString(reader["uuid"]),
                    name = Convert.ToString(reader["name"]),
                    description = Convert.ToString(reader["description"]),
                    creationDate = Convert.ToDateTime(reader["creationDate"]),
                    department = new Department() {
                        uuid = Convert.ToString(reader["departmentUUID"])
                    },
                    creator = new User() {
                        uuid = Convert.ToString(reader["creatorUUID"])
                    }
                });
            }

            return subjects;
        }

        public Subject MapToSubject(IDataReader reader) {
            while (reader.Read()) {
                return new Subject() {
                    id = Convert.ToInt32(reader["id"]),
                    uuid = Convert.ToString(reader["uuid"]),
                    name = Convert.ToString(reader["name"]),
                    description = Convert.ToString(reader["description"]),
                    creationDate = Convert.ToDateTime(reader["creationDate"]),
                    department = new Department() {
                        uuid = Convert.ToString(reader["departmentUUID"])
                    },
                    creator = new User() {
                        uuid = Convert.ToString(reader["creatorUUID"])
                    }
                };
            }

            return null;
        }

		public List<SubjectArchive> MapToSubjectArchives(IDataReader reader)
		{
			List<SubjectArchive> archives = new List<SubjectArchive>();

			while (reader.Read())
			{
				archives.Add(new SubjectArchive()
				{
					subjectUUID = Convert.ToString(reader["subjectUUID"]),
					name = Convert.ToString(reader["name"]),
					description = Convert.ToString(reader["description"]),
					creationDate = Convert.ToDateTime(reader["creationDate"]),
					departmentUUID = Convert.ToString(reader["departmentUUID"]),
					creatorUUID = Convert.ToString(reader["creatorUUID"]),
					moderatorUUID = Convert.ToString(reader["moderatorUUID"]),
					changeDate = Convert.ToDateTime(reader["changeDate"]),
					version = Convert.ToInt32(reader["version"])
				});
			}

			return archives;
		}


		public SubjectArchive MapToSubjectArchive(IDataReader reader)
		{
			while (reader.Read())
			{
				return new SubjectArchive()
				{
					subjectUUID = Convert.ToString(reader["subjectUUID"]),
					name = Convert.ToString(reader["name"]),
					description = Convert.ToString(reader["description"]),
					creationDate = Convert.ToDateTime(reader["creationDate"]),
					departmentUUID = Convert.ToString(reader["departmentUUID"]),
					creatorUUID = Convert.ToString(reader["creatorUUID"]),
					moderatorUUID = Convert.ToString(reader["moderatorUUID"]),
					changeDate = Convert.ToDateTime(reader["changeDate"]),
					version = Convert.ToInt32(reader["version"])
				};
			}
			return null;
		}

		public SubjectArchive MapToSubjectArchiveAfterInsert(IDataReader reader)
		{
			reader.Read();
			return new SubjectArchive()
			{
				subjectUUID = Convert.ToString(reader["subjectUUID"]),
				name = Convert.ToString(reader["name"]),
				description = Convert.ToString(reader["description"]),
				creationDate = Convert.ToDateTime(reader["creationDate"]),
				departmentUUID = Convert.ToString(reader["departmentUUID"]),
				creatorUUID = Convert.ToString(reader["creatorUUID"]),
				moderatorUUID = Convert.ToString(reader["moderatorUUID"]),
				changeDate = Convert.ToDateTime(reader["changeDate"]),
				version = Convert.ToInt32(reader["version"])
			};
		}
	}
}

