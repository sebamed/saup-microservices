using System;
using System.Net;

namespace Commons.ExceptionHandling.Exceptions {
    public class PayloadTooLargeException : BaseException {

        public PayloadTooLargeException(string message, string origin) : base(message, origin, HttpStatusCode.RequestEntityTooLarge) {
            this.message = message;
            this.origin = origin;
        }
    }
}