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



        internal List<CourseArchive> MapToCourseArchives(IDataReader reader)
        {
            List<CourseArchive> archives = new List<CourseArchive>();

            while (reader.Read())
            {
                archives.Add(new CourseArchive()
                {
                    name = Convert.ToString(reader["name"]),
                    description = Convert.ToString(reader["description"]),
                    active = Convert.ToBoolean(reader["active"]),
                    maxStudents = Convert.ToInt32(reader["maxStudents"]),
                    minStudents = Convert.ToInt32(reader["minStudents"]),
                    creationDate = Convert.ToDateTime(reader["creationDate"]),
                    subjectUUID = Convert.ToString(reader["subjectUUID"]),
                    moderatorUUID = Convert.ToString(reader["moderatorUUID"]),
                    changeDate = Convert.ToDateTime(reader["changeDate"]),
                    version = Convert.ToInt32(reader["version"])
                });
            }
            return archives;
        }

        internal List<CourseStudent> MapToCourseStudents(IDataReader reader)
        {
            List<CourseStudent> students = new List<CourseStudent>();

            while (reader.Read())
            {
                students.Add(new CourseStudent()
                {
                    studentUUID = Convert.ToString(reader["studentUUID"]),
                    courseUUID = Convert.ToString(reader["courseUUID"]),
                    activeStudent = Convert.ToBoolean(reader["activeStudent"]),
                    currentPoints = (float)Convert.ToDouble(reader["currentPoints"]),
                    finalMark = Convert.ToInt32(reader["finalMark"]),
                    beginDate = Convert.ToDateTime(reader["beginDate"])
                });
            }
            return students;
        }
        internal CourseStudent MapToCourseStudent(IDataReader reader)
        {
            reader.Read();
            return new CourseStudent()
            {
                studentUUID = Convert.ToString(reader["studentUUID"]),
                courseUUID = Convert.ToString(reader["courseUUID"]),
                activeStudent = Convert.ToBoolean(reader["activeStudent"]),
                currentPoints = (float)Convert.ToDouble(reader["currentPoints"]),
                finalMark = Convert.ToInt32(reader["finalMark"]),
                beginDate = Convert.ToDateTime(reader["beginDate"])
            };
        }

        internal List<CourseTeacher> MapToCourseTeachers(IDataReader reader)
        {
            List<CourseTeacher> teachers = new List<CourseTeacher>();

            while (reader.Read())
            {
                teachers.Add(new CourseTeacher()
                {
                    teacherUUID = Convert.ToString(reader["teacherUUID"]),
                    activeTeacher = Convert.ToBoolean(reader["activeTeacher"])
                });
            }
            return teachers;
        }
        internal CourseTeacher MapToCourseTeacher(IDataReader reader)
        {
            reader.Read();
            return new CourseTeacher()
            {
                teacherUUID = Convert.ToString(reader["teacherUUID"]),
                activeTeacher = Convert.ToBoolean(reader["activeTeacher"]),
                courseUUID = Convert.ToString(reader["courseUUID"])
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
