using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Domain {
    public class AuthenticatedUser : BaseEntity {

        public string email { get; set; }

        public string role { get; set; }

    }
}
