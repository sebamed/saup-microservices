using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_USER_BASE = ROUTE_API_BASE + "/users";
        public const string ROUTE_USER_GET_ONE_BY_UUID = ROUTE_USER_BASE + "/{uuid}";

        public const string ROUTE_ADMIN_BASE = ROUTE_USER_BASE + "/admins";
        public const string ROUTE_ADMIN_GET_ONE_BY_UUID = ROUTE_ADMIN_BASE + "/{uuid}";

        public const string ROUTE_STUDENT_BASE = ROUTE_USER_BASE + "/students";
        public const string ROUTE_STUDENT_GET_ONE_BY_UUID = ROUTE_STUDENT_BASE + "/{uuid}";

        public const string ROUTE_TEACHER_BASE = ROUTE_USER_BASE + "/teachers";
        public const string ROUTE_TEACHER_GET_ONE_BY_UUID = ROUTE_TEACHER_BASE + "/{uuid}";
    }
}
