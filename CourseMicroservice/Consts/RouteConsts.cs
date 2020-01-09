using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_COURSE_BASE = "/courses";
        public const string ROUTE_COURSE_GET_ONE_BY_UUID = ROUTE_COURSE_BASE + "/{uuid}";

    }
}
