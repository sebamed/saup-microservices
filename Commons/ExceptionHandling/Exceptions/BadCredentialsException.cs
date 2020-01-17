using System.Net;

namespace Commons.ExceptionHandling.Exceptions {
    public class BadCredentialsException : BaseException {

        public BadCredentialsException(string message, string origin) : base(message, origin, HttpStatusCode.Unauthorized) {
            this.message = message;
            this.origin = origin;
            this.code = HttpStatusCode.Unauthorized;
        }

    }
}
