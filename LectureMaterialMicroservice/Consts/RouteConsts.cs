using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LectureMaterialMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_SECTION_BASE = ROUTE_API_BASE + "/lecture-materials";
        public const string ROUTE_SECTION_GET_ONE_BY_UUID = ROUTE_SECTION_BASE + "/{uuid}";
        public const string ROUTE_SECTION_GET_VISIBLE = ROUTE_SECTION_BASE + "/visible";

        public const string ROUTE_SECTION_ARCHIVE_BASE = ROUTE_API_BASE + "/lecture-materials/archive";
        public const string ROUTE_SECTION_GET_ARCHIVE_BY_SECTION_UUID = ROUTE_SECTION_ARCHIVE_BASE + "/{sectionUUID}";
        public const string ROUTE_SECTION_GET_VISIBLE_IN_ARCHIVE = ROUTE_SECTION_ARCHIVE_BASE + "/visible";
    }
}
