using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_MESSAGING_BASE = ROUTE_API_BASE + "/messaging";

        public const string ROUTE_MESSAGING_BY_RECIPIENT = ROUTE_MESSAGING_BASE + "/conversation";
    }
}
