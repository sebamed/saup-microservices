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
        
        //Course Archives
        public const string ROUTE_COURSE_ARCHIVES = ROUTE_COURSE_BASE + "/archives/{uuid}";
    }
}
