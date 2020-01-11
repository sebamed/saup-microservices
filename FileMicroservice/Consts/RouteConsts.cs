using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_FILE_BASE = ROUTE_API_BASE + "/files";

        public const string ROUTE_FILE_GET_ONE_BY_UUID = ROUTE_FILE_BASE + "/{uuid}";
        public const string ROUTE_FILE_BY_PATH = ROUTE_FILE_BASE + "/path/{path}";

    }
}
