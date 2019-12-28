using System.Net;

namespace Commons.ExceptionHandling.Exceptions {
    public class EntityNotFoundException : BaseException {

        public EntityNotFoundException(string message, string origin) : base(message, origin, HttpStatusCode.NotFound) {
            this.message = message;
            this.origin = origin;
            this.code = HttpStatusCode.NotFound;
        }

    }
}
