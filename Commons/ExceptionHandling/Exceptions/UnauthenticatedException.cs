using System;
using System.Net;

namespace Commons.ExceptionHandling.Exceptions {
    public class UnauthenticatedException : BaseException {

        public UnauthenticatedException(string message, string origin) : base(message, origin, HttpStatusCode.Unauthorized) {
            this.message = message;
            this.origin = origin;
            this.code = HttpStatusCode.Unauthorized;
        }

    }
}
