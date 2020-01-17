using System.Net;

namespace Commons.ExceptionHandling.Exceptions {
    public class EntityAlreadyExistsException : BaseException {

        public EntityAlreadyExistsException(string message, string origin) : base(message, origin, HttpStatusCode.Conflict) {
            this.message = message;
            this.origin = origin;
            this.code = HttpStatusCode.Conflict;
        }

    }
}
