using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Configuration {
    public class EmailSenderConfigurationModel {

        public string from { get; set; }

        public string username { get; set; }

        public string password { get; set; }

    }
}
