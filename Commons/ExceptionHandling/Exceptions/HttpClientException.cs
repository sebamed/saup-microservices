using System.Net;

namespace Commons.ExceptionHandling.Exceptions {
    public class HttpClientException : BaseException {

        public HttpClientException(string message, string origin) : base(message, origin, HttpStatusCode.BadGateway) {
            this.message = message;
            this.origin = origin;
            this.code = HttpStatusCode.BadGateway;
        }

    }
}
