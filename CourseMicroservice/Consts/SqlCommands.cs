
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
            $"set name = '{course.name}', description = '{course.description}', active = {boolToInt(course.active)}, maxStudents = {course.maxStudents}, minStudents = {course.minStudents}, " +
            $"creationDate = '{course.creationDate}', subjectUUID = '{course.subject.uuid}'" +
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
                $"subjectUUID) output inserted.* " +
                $"values('{course.uuid}', '{course.name}', '{course.description}', {boolToInt(course.active)}, {course.maxStudents}, {course.minStudents}, '{course.creationDate}'," +
                $"'{course.subject.uuid}');";
        }

        //COURSE TEACHERS
        public string GET_ACTIVE_COURSE_TEACHERS(string uuid)
        {
            return $"select * from SAUP_COURSE.TeacherCourse where courseUUID='{uuid}' and activeTeacher=1";
        }
        public string GET_ONE_COURSE_TEACHER(string courseUuid, string teacherUuid)
        {
            return $"select * from SAUP_COURSE.TeacherCourse where courseUUID='{courseUuid}' and teacherUUID='{teacherUuid}'";
        }
        public string GET_ALL_COURSES_FROM_STUDENT(string studentUuid)
        {
            return $"select * from SAUP_COURSE.StudentCourse where studentUUID = '{studentUuid}' and activeStudent = 1;";
        }
        public string UPDATE_TEACHER_COURSE(CourseTeacher courseTeacher)
        {
            return $"update SAUP_COURSE.TeacherCourse set activeTeacher = {boolToInt(courseTeacher.activeTeacher)} output inserted.* where teacherUUID = '{courseTeacher.teacher.uuid}' and courseUUID = '{courseTeacher.course.uuid}';";
        }
        public string CREATE_TEACHER_COURSE(CourseTeacher courseTeacher)
        {
            return $"insert into SAUP_COURSE.TeacherCourse(teacherUUID, courseUUID, activeTeacher) output inserted.* " +
            $"values('{courseTeacher.teacher.uuid}', '{courseTeacher.course.uuid}', {boolToInt(courseTeacher.activeTeacher)} );";
        }

        //COURSE STUDENTS
        public string GET_ACTIVE_COURSE_STUDENTS(string uuid)
        {
            return $"select * from SAUP_COURSE.StudentCourse where courseUUID='{uuid}' and activeStudent=1;";
        }
        public string GET_ONE_COURSE_STUDENT(string courseUuid, string studentUuid)
        {
            return $"select * from SAUP_COURSE.StudentCourse where courseUUID='{courseUuid}' and studentUUID='{studentUuid}';";
        }
        public string UPDATE_STUDENT_ON_COURSE(CourseStudent courseStudent)
        {
            return $"update SAUP_COURSE.StudentCourse set activeStudent = {boolToInt(courseStudent.activeStudent)}, beginDate = '{courseStudent.beginDate}', currentPoints = {courseStudent.currentPoints}, finalMark = {courseStudent.finalMark} " +
            $"output inserted.* where studentUUID = '{courseStudent.student.uuid}' and courseUUID = '{courseStudent.course.uuid}'; ";
        }
        public string CREATE_STUDENT_ON_COURSE(CourseStudent courseStudent)
        {
            return $"insert into SAUP_COURSE.StudentCourse(studentUUID, courseUUID, activeStudent, beginDate, currentPoints, finalMark) " +
            $"output inserted.* values('{courseStudent.student.uuid}', '{courseStudent.course.uuid}', {boolToInt(courseStudent.activeStudent)}, '{courseStudent.beginDate}', {courseStudent.currentPoints}, {courseStudent.finalMark}); ";
        }

        //COURSE ARCHIVES
        public string GET_COURSE_ARCHIVES(string uuid)
        {
            return $"select * from SAUP_COURSE.CourseArchive where courseUUID = '{uuid}';";
        }
        public string CREATE_COURSE_ARCHIVE(CourseArchive courseArchive)
        {
            return $"insert into SAUP_COURSE.CourseArchive(courseUUID, name, description, active, maxStudents, minStudents, creationDate, subjectUUID, moderatorUUID, changeDate) " +
           $"values('{courseArchive.courseUUID}', '{courseArchive.name}', '{courseArchive.description}', {boolToInt(courseArchive.active)}, {courseArchive.maxStudents}, " +
           $"{courseArchive.minStudents}, '{courseArchive.creationDate}', '{courseArchive.subject.uuid}', '{courseArchive.moderator.uuid}', '{courseArchive.changeDate}'); ";
        }

        //COURSE STATISTICS
        public string GET_COURSE_STATISTICS_COURSE_UUID(string courseUuid)
        {
            return $"select avg(finalMark) as average, min(finalMark) as minimum, max(finalMark) as maximum " +
            $"from SAUP_COURSE.StudentCourse where courseUUID = '{courseUuid}'; ";
        }
        public string GET_COURSE_STATISTICS_STUDENT_UUID(string studentUuid)
        {
            return $"select avg(finalMark) as average, min(finalMark) as minimum, max(finalMark) as maximum " +
            $"from SAUP_COURSE.StudentCourse where studentUUID = '{studentUuid}'; ";
        }
        public string GET_COURSE_STATISTCS_YEAR(int year)
        {
            return $"select avg(finalMark) as average, min(finalMark) as minimum, max(finalMark) as maximum " +
            $"from SAUP_COURSE.StudentCourse where year(beginDate) = {year}; ";
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
