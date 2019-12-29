using System.Net;

namespace Commons.ExceptionHandling.Exceptions {
    public class BadSqlQueryException : BaseException {

        public BadSqlQueryException(string message, string origin) : base(message, origin, HttpStatusCode.NotImplemented) {
            this.message = message;
            this.origin = origin;
            this.code = HttpStatusCode.NotImplemented;
        }

    }
}
