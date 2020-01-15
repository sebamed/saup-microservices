using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";
        
        //Course
        public const string ROUTE_COURSE_BASE = ROUTE_API_BASE + "/courses";
        public const string ROUTE_COURSE_GET_ONE_BY_UUID = ROUTE_COURSE_BASE + "/{uuid}";
        
        //Course Teachers
        public const string ROUTE_COURSE_TEACHERS = ROUTE_COURSE_BASE + "/teachers";
        public const string ROUTE_COURSE_TEACHERS_BY_UUID = ROUTE_COURSE_TEACHERS + "/{uuid}";
        
        //Course Students
        public const string ROUTE_COURSE_STUDENTS = ROUTE_COURSE_BASE + "/students";
        public const string ROUTE_COURSE_STUDENTS_BY_UUID = ROUTE_COURSE_STUDENTS + "/{uuid}";
        public const string ROUTE_COURSE_STUDENTS_BY_STUDENT_UUID = ROUTE_COURSE_STUDENTS + "/student/{studentuuid}";
        
        //Course Archives
        public const string ROUTE_COURSE_ARCHIVES = ROUTE_COURSE_BASE + "/archives/{uuid}";

        //Course Statistics
        public const string ROUTE_COURSE_STATISTICS_BASE = ROUTE_COURSE_BASE + "/statistics";
        public const string ROUTE_COURSE_STATISTICS_BY_COURSE_UUID = ROUTE_COURSE_STATISTICS_BASE + "/course/{courseuuid}";
        public const string ROUTE_COURSE_STATISTICS_BY_STUDENT_UUID = ROUTE_COURSE_STATISTICS_BASE + "/student/{studentuuid}";
        public const string ROUTE_COURSE_STATISTICS_BY_YEAR = ROUTE_COURSE_STATISTICS_BASE + "/year/{year}";
    }
}
