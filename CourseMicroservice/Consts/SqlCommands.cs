
using CourseMicroservice.Domain;

namespace CourseMicroservice.Consts {
    public class SqlCommands {
        
        //GET COMMANDS

        public string GET_ALL_COURSES()
        {
            return $"select * from SAUP_COURSE.Course;";
        }
        public string GET_ONE_COURSE_BY_UUID(string uuid)
        {
            return $"select * from SAUP_COURSE.Course where uuid = '{uuid}';";
        }
        public string GET_COURSE_TEACHERS(string uuid)
        {
            return $"select * from SAUP_COURSE.TeacherCourse where courseUUID='{uuid}'";
        }
        public string GET_COURSE_STUDENTS(string uuid)
        {
            return $"select * from SAUP_COURSE.StudentCourse where courseUUID='{uuid}'";
        }
        public string GET_COURSE_ARCHIVES(string uuid)
        {
            return $"select * from SAUP_COURSE.CourseArchive where courseUUID = '{uuid}';";
        }


        //INSERT COMMANDS
        public string CREATE_COURSE(Course course)
        {
            //SQL Server ne poznaje boolean, samo bit koji moze biti 1 ili 0
            int var = 0;
            if (course.active == true) {
                var = 1;
            }
            return $"insert into SAUP_COURSE.Course(uuid, name, description, active, maxStudents, minStudents, creationDate," +
                $"subjectUUID) output inserted.* values('{course.uuid}', '{course.name}', '{course.description}', {var}, {course.maxStudents}, {course.minStudents}, '{course.creationDate}'," +
                $"'{course.subjectUUID}');";
        }

 



        //UPDATE COMMANDS
        public string UPDATE_COURSE(Course course)
        {
            int var = 0;
            if (course.active == true)
            {
                var = 1;
            }
            return $"update SAUP_COURSE.Course " +
            $"set name = '{course.name}', description = '{course.description}', active = {var}, maxStudents = {course.maxStudents}, minStudents = {course.minStudents}, creationDate = '{course.creationDate}'" +
            $" output inserted.* where uuid = '{course.uuid}';";
        }


        //DELETE COMMANDS
        public string DELETE_COURSE(string uuid)
        {
            return $"delete from SAUP_Course.Course output deleted.* " +
            $"where uuid = '{uuid}';";
        }

    }
}
