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
                    subject = new Subject()
                    {
                        uuid = Convert.ToString(reader["subjectUUID"])
                    }
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
                    subject = new Subject()
                    {
                        uuid = Convert.ToString(reader["subjectUUID"])
                    }
                };
            }
            return null;
        }

        internal CourseStatistics MapToCourseStatistics(IDataReader reader)
        {
            reader.Read();
            return new CourseStatistics()
            {
                average = Convert.ToInt32(reader["average"]),
                min = Convert.ToInt32(reader["minimum"]),
                max = Convert.ToInt32(reader["maximum"])
            };
        }

        public CourseArchive MapToCourseArchive(IDataReader reader)
        {
            reader.Read();
            return new CourseArchive()
            {
                name = Convert.ToString(reader["name"]),
                description = Convert.ToString(reader["description"]),
                active = Convert.ToBoolean(reader["active"]),
                maxStudents = Convert.ToInt32(reader["maxStudents"]),
                minStudents = Convert.ToInt32(reader["minStudents"]),
                creationDate = Convert.ToDateTime(reader["creationDate"]),
                subject = new Subject()
                {
                    uuid = Convert.ToString(reader["subjectUUID"])
                },
                moderator = new Teacher()
                {
                    uuid = Convert.ToString(reader["moderatorUUID"])
                },
                changeDate = Convert.ToDateTime(reader["changeDate"]),
                version = Convert.ToInt32(reader["version"]),
                courseUUID = Convert.ToString(reader["courseUUID"])
            };
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
                subject = new Subject()
                {
                    uuid = Convert.ToString(reader["subjectUUID"])
                }
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
                    subject = new Subject()
                    {
                        uuid = Convert.ToString(reader["subjectUUID"])
                    },
                    moderator = new Teacher()
                    {
                        uuid = Convert.ToString(reader["moderatorUUID"])
                    },
                    changeDate = Convert.ToDateTime(reader["changeDate"]),
                    version = Convert.ToInt32(reader["version"]),
                    courseUUID = Convert.ToString(reader["courseUUID"])
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
                    student = new Student()
                    {
                        uuid = Convert.ToString(reader["studentUUID"])
                    },
                    course = new Course()
                    {
                        uuid = Convert.ToString(reader["courseUUID"])
                    },
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
                student = new Student()
                {
                    uuid = Convert.ToString(reader["studentUUID"])
                },
                course = new Course()
                {
                    uuid = Convert.ToString(reader["courseUUID"])
                },
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
                    teacher = new Teacher()
                    {
                        uuid = Convert.ToString(reader["teacherUUID"])
                    },
                    course = new Course()
                    {
                        uuid = Convert.ToString(reader["courseUUID"])
                    },
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
                teacher = new Teacher()
                {
                    uuid = Convert.ToString(reader["teacherUUID"])
                },
                course = new Course()
                {
                    uuid = Convert.ToString(reader["courseUUID"])
                },
                activeTeacher = Convert.ToBoolean(reader["activeTeacher"]),
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
                subject = new Subject()
                {
                    uuid = Convert.ToString(reader["subjectUUID"])
                }
            };
        }
    }
}
