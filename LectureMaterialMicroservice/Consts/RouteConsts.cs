using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LectureMaterialMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_SECTION_BASE = ROUTE_API_BASE + "/sections";
        public const string ROUTE_SECTION_GET_ONE_BY_UUID = ROUTE_SECTION_BASE + "/{uuid}";
        public const string ROUTE_SECTION_GET_VISIBLE = ROUTE_SECTION_BASE + "/visible";
        public const string ROUTE_SECTION_GET_BY_COURSE = ROUTE_SECTION_BASE + "/course/{uuid}";
        public const string ROUTE_SECTION_GET_VISIBLE_BY_COURSE = ROUTE_SECTION_GET_VISIBLE + "/course/{uuid}";

        public const string ROUTE_SECTION_ARCHIVE_BASE = ROUTE_API_BASE + "/sections/archive";
        public const string ROUTE_ARCHIVES_BY_SECTION_UUID = ROUTE_SECTION_ARCHIVE_BASE + "/{uuid}";
        public const string ROUTE_LATEST_ARCHIVE_BY_SECTION_UUID = ROUTE_SECTION_ARCHIVE_BASE + "/latest/{uuid}";

        public const string ROUTE_MATERIAL_BASE = ROUTE_API_BASE + "/sections/material";
    }
}