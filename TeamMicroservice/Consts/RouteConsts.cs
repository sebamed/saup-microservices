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
        public const string ROUTE_STUDENT_TEAM_BASE = ROUTE_TEAM_BASE + "/students";
        public const string ROUTE_STUDENT_TEAM_BY_UUID = ROUTE_STUDENT_TEAM_BASE + "/{uuid}";
    }
}
