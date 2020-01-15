using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_MESSAGING_BASE = ROUTE_API_BASE + "/messaging";

        public const string ROUTE_MESSAGING_BY_RECIPIENT = ROUTE_MESSAGING_BASE + "/conversation";

        public const string ROUTE_MESSAGING_UPDATE_FILE = ROUTE_MESSAGING_BASE + "/file";

        public const string ROUTE_MESSAGING_UPDATE_RECIPIENT = ROUTE_MESSAGING_BASE + "/recipient";

        public const string ROUTE_MESSAGING_UPDATE_SENDER = ROUTE_MESSAGING_BASE + "/sender";
    }
}
