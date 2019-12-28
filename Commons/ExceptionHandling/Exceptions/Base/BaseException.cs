using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Commons.ExceptionHandling.Exceptions {
    public class BaseException : Exception {

        public string message { get; set; }

        public string origin { get; set; }

        public HttpStatusCode code { get; set; }

        public override string Message {
            get { return this.message; }
        }

        public BaseException(string message, string origin, HttpStatusCode code) {
            this.message = message;
            this.origin = origin;
            this.code = code;
        }



    }
}
