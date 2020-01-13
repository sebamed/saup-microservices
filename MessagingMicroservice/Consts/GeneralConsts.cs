using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.Consts {
    public class GeneralConsts {
        public const string MICROSERVICE_NAME = "MESSAGING Microservice";

        public const string SCHEMA_NAME = "SAUP_MESSAGING";

        public const string MESSAGE_TABLE = SCHEMA_NAME + ".Message";

        public const string RECIPIENT_TABLE = SCHEMA_NAME + ".Recipient";

        public const string FILE_MESSAGE_TABLE = SCHEMA_NAME + ".FileMessage";
    }
}
