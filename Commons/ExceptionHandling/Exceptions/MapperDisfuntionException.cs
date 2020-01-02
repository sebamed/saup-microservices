using System.Net;

namespace Commons.ExceptionHandling.Exceptions {
    public class MapperDisfuntionException : BaseException {

        public MapperDisfuntionException(string message, string origin) : base(message, origin, HttpStatusCode.ServiceUnavailable) {
            this.message = message;
            this.origin = origin;
            this.code = HttpStatusCode.ServiceUnavailable;
        }

    }
}
