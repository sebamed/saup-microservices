using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_DEPARTMENT_BASE = ROUTE_API_BASE + "/departments";
        public const string ROUTE_FACULTY_BASE = ROUTE_DEPARTMENT_BASE + "/faculties";
    }
}
