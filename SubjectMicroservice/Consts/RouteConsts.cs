using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_SUBJECT_BASE = ROUTE_API_BASE + "/subjects";
        public const string ROUTE_SUBJECT_BY_UUID = ROUTE_SUBJECT_BASE + "/{uuid}";
        public const string ROUTE_SUBJECT_BY_NAME = ROUTE_SUBJECT_BASE + "/name/{name}";
        public const string ROUTE_SUBJECT_BY_DEPARTMENT_UUID = ROUTE_SUBJECT_BASE + "/department/{uuid}";
        public const string ROUTE_SUBJECT_BY_CREATOR_UUID = ROUTE_SUBJECT_BASE + "/creator/{uuid}";

        public const string ROUTE_SUBJECT_ARCHIVE_BASE = ROUTE_SUBJECT_BASE + "/archives";
        public const string ROUTE_SUBJECT_ARCHIVE_BY_UUID = ROUTE_SUBJECT_ARCHIVE_BASE + "/{uuid}";
        public const string ROUTE_SUBJECT_ARCHIVE_BY_NAME = ROUTE_SUBJECT_ARCHIVE_BASE + "/name/{name}";

    }
}
