using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CourseMicroservice.Domain;

namespace CourseMicroservice.Mappers
{
    public class ModelMapper
    {
        public List<Course> MapToCourses(IDataReader reader)
        {
            List<Course> courses = new List<Course>();

            while (reader.Read())
            {
                courses.Add(new Course()
                {
                    uuid = Convert.ToString(reader["uuid"]),
                    id = Convert.ToInt32(reader["id"]),
                    name = Convert.ToString(reader["name"]),
                    description = Convert.ToString(reader["description"]),
                    active = Convert.ToBoolean(reader["active"]),
                    maxStudents = Convert.ToInt32(reader["maxStudents"]),
                    minStudents = Convert.ToInt32(reader["minStudents"]),
                    creationDate = Convert.ToDateTime(reader["creationDate"]),
                    subjectUUID = Convert.ToString(reader["subjectUUID"])
                });
            }
            return courses;
        }

        public Course MapToCourse(IDataReader reader)
        {
            while (reader.Read())
            {
                return new Course()
                {
                    uuid = Convert.ToString(reader["uuid"]),
                    id = Convert.ToInt32(reader["id"]),
                    name = Convert.ToString(reader["name"]),
                    description = Convert.ToString(reader["description"]),
                    active = Convert.ToBoolean(reader["active"]),
                    maxStudents = Convert.ToInt32(reader["maxStudents"]),
                    minStudents = Convert.ToInt32(reader["minStudents"]),
                    creationDate = Convert.ToDateTime(reader["creationDate"]),
                    subjectUUID = Convert.ToString(reader["subjectUUID"])
                };
            }
            return null;
        }
        public Course MapToCourseAfterInsert(IDataReader reader)
        {
            reader.Read();
            return new Course()
            {
                uuid = Convert.ToString(reader["uuid"]),
                id = Convert.ToInt32(reader["id"]),
                name = Convert.ToString(reader["name"]),
                description = Convert.ToString(reader["description"]),
                active = Convert.ToBoolean(reader["active"]),
                maxStudents = Convert.ToInt32(reader["maxStudents"]),
                minStudents = Convert.ToInt32(reader["minStudents"]),
                creationDate = Convert.ToDateTime(reader["creationDate"]),
                subjectUUID = Convert.ToString(reader["subjectUUID"])
            };
        }
        public Course MapToCourseAfterUpdate(IDataReader reader)
        {
            reader.Read();
            return new Course()
            {
                uuid = Convert.ToString(reader["uuid"]),
                id = Convert.ToInt32(reader["id"]),
                name = Convert.ToString(reader["name"]),
                description = Convert.ToString(reader["description"]),
                active = Convert.ToBoolean(reader["active"]),
                maxStudents = Convert.ToInt32(reader["maxStudents"]),
                minStudents = Convert.ToInt32(reader["minStudents"]),
                creationDate = Convert.ToDateTime(reader["creationDate"]),
                subjectUUID = Convert.ToString(reader["subjectUUID"])
            };
        }
    }
}
