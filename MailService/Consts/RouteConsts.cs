using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_MAIL_BASE = ROUTE_API_BASE + "/mails";

        public const string ROUTE_MAIL_SEND = ROUTE_MAIL_BASE + "/send";
    }
}
