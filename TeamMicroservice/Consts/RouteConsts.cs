using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_TEAM_BASE = ROUTE_API_BASE + "/teams";
        public const string ROUTE_TEAM_BY_UUID = ROUTE_TEAM_BASE + "/{uuid}";
        public const string ROUTE_TEAM_BY_NAME = ROUTE_TEAM_BASE + "/name/{name}";
        public const string ROUTE_TEAM_BY_COURSE = ROUTE_TEAM_BASE + "/course/{uuid}";

        public const string ROUTE_ADD_STUDENT_INTO_TEAM = ROUTE_TEAM_BASE + "/students";
    }
}
