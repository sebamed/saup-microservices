using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_DEPARTMENT_BASE = ROUTE_API_BASE + "/departments";
		public const string ROUTE_DEPARTMENT_BY_UUID = ROUTE_DEPARTMENT_BASE + "/{uuid}";
		public const string ROUTE_DEPARTMENT_BY_NAME = ROUTE_DEPARTMENT_BASE + "/name/{name}";
        public const string ROUTE_DEPARTMENT_BY_FACULTY_NAME = ROUTE_DEPARTMENT_BASE + "/faculty/{facultyName}";
        public const string ROUTE_FACULTY_BASE = ROUTE_DEPARTMENT_BASE + "/faculties";
        public const string ROUTE_FACULTY_BY_UUID = ROUTE_FACULTY_BASE + "/{uuid}";
        public const string ROUTE_FACULTY_BY_NAME = ROUTE_FACULTY_BASE + "/name/{name}";
        public const string ROUTE_FACULTY_BY_CITY = ROUTE_FACULTY_BASE + "/city/{city}";
    }
}
