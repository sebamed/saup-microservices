using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";
        //Course
        public const string ROUTE_COURSE_BASE = "/courses";
        public const string ROUTE_COURSE_GET_ONE_BY_UUID = ROUTE_COURSE_BASE + "/{uuid}";
        //Course Teachers
        public const string ROUTE_COURSE_TEACHERS = ROUTE_COURSE_GET_ONE_BY_UUID + "/teachers";
        public const string ROUTE_COURSE_TEACHERS_GET_ONE_BY_UUID = ROUTE_COURSE_TEACHERS + "/{teacherUuid}";
        //Course Students
        public const string ROUTE_COURSE_STUDENTS = ROUTE_COURSE_GET_ONE_BY_UUID + "/students";
        public const string ROUTE_COURSE_STUDENTS_GET_ONE_BY_UUID = ROUTE_COURSE_STUDENTS + "/{studentUuid}";
    }
}
