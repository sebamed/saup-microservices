
using CourseMicroservice.Domain;

namespace CourseMicroservice.Consts {
    public class SqlCommands {
        
        //COURSE COMMANDS
        public string GET_ALL_COURSES()
        {
            return $"select * from SAUP_COURSE.Course;";
        }
        public string GET_ONE_COURSE_BY_UUID(string uuid)
        {
            return $"select * from SAUP_COURSE.Course where uuid = '{uuid}';";
        }
        public string UPDATE_COURSE(Course course)
        {
            return $"update SAUP_COURSE.Course " +
            $"set name = '{course.name}', description = '{course.description}', active = {boolToInt(course.active)}, maxStudents = {course.maxStudents}, minStudents = {course.minStudents}, creationDate = '{course.creationDate}'" +
            $" output inserted.* where uuid = '{course.uuid}';";
        }
        public string DELETE_COURSE(string uuid)
        {
            return $"delete from SAUP_Course.Course output deleted.* " +
            $"where uuid = '{uuid}';";
        }
        public string CREATE_COURSE(Course course)
        {
            return $"insert into SAUP_COURSE.Course(uuid, name, description, active, maxStudents, minStudents, creationDate," +
                $"subjectUUID) output inserted.* values('{course.uuid}', '{course.name}', '{course.description}', {boolToInt(course.active)}, {course.maxStudents}, {course.minStudents}, '{course.creationDate}'," +
                $"'{course.subjectUUID}');";
        }

        //COURSE TEACHERS
        public string GET_COURSE_TEACHERS(string uuid)
        {
            return $"select * from SAUP_COURSE.TeacherCourse where courseUUID='{uuid}'";
        }
        public string GET_ONE_COURSE_TEACHER(string courseUuid, string teacherUuid)
        {
            return $"select * from SAUP_COURSE.TeacherCourse where courseUUID='{courseUuid}' and teacherUUID='{teacherUuid}'";
        }
        public string UPDATE_TEACHER_COURSE(CourseTeacher courseTeacher)
        {
            return $"update SAUP_COURSE.TeacherCourse set activeTeacher = {boolToInt(courseTeacher.activeTeacher)} output inserted.* where teacherUUID = '{courseTeacher.teacherUUID}' and courseUUID = '{courseTeacher.courseUUID}';";
        }
        public string DELETE_TEACHER_COURSE(string courseUUID, string teacherUUID)
        {
            return $"delete from SAUP_COURSE.TeacherCourse output deleted.* where courseUUID = '{courseUUID}' and teacherUUID = '{teacherUUID}';";
        }
        public string CREATE_TEACHER_COURSE(CourseTeacher courseTeacher)
        {
            return $"insert into SAUP_COURSE.TeacherCourse(teacherUUID, courseUUID, activeTeacher) output inserted.* " +
            $"values('{courseTeacher.teacherUUID}', '{courseTeacher.courseUUID}', {courseTeacher.activeTeacher} );";
        }

        //COURSE STUDENTS
        public string GET_COURSE_STUDENTS(string uuid)
        {
            return $"select * from SAUP_COURSE.StudentCourse where courseUUID='{uuid}';";
        }
        public string GET_ONE_COURSE_STUDENT(string courseUuid, string studentUuid)
        {
            return $"select * from SAUP_COURSE.StudentCourse where courseUUID='{courseUuid}' and studentUUID='{studentUuid}';";
        }
        public string UPDATE_STUDENT_ON_COURSE(CourseStudent courseStudent)
        {
            return $"update SAUP_COURSE.StudentCourse set activeStudent = {boolToInt(courseStudent.activeStudent)}, beginDate = '{courseStudent.beginDate}', currentPoints = {courseStudent.currentPoints}, finalMark = {courseStudent.finalMark} " +
            $"output inserted.* where studentUUID = '{courseStudent.studentUUID}' and courseUUID = '{courseStudent.courseUUID}'; ";
        }
        public string CREATE_STUDENT_ON_COURSE(CourseStudent courseStudent)
        {
            return $"insert into SAUP_COURSE.StudentCourse(studentUUID, courseUUID, activeStudent, beginDate, currentPoints, finalMark) " +
            $"output inserted.* values('{courseStudent.studentUUID}', '{courseStudent.courseUUID}', {boolToInt(courseStudent.activeStudent)}, '{courseStudent.beginDate}', {courseStudent.currentPoints}, {courseStudent.finalMark}); ";
        }
        public string DELETE_STUDENT_COURSE(string courseUUID, string teacherUUID)
        {
            return $"delete from SAUP_COURSE.StudentCourse output deleted.* where courseUUID = '{courseUUID}' and studentUUID = '{teacherUUID}';";
        }


        //HELP METHOD
        public int boolToInt(bool bl)
        {
            if(bl == true)
            {
                return 1;
            }
            return 0;
        }

    }
}
