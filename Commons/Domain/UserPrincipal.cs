using Commons.Consts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Security.Claims;

namespace Commons.Domain {
    public class UserPrincipal {

        private ClaimsPrincipal _principal;

        public StringValues token { get; }

        public string uuid {
            get {
                return this._principal.Claims.First(c => c.Type == JwtClaimsCustomConsts.Sub).Value;
            }
        }

        public string role {
            get {
                return this._principal.Claims.First(c => c.Type == JwtClaimsCustomConsts.Role).Value;
            }
        }

        public UserPrincipal(HttpContext context) {
            this._principal = context.User;

            StringValues sToken = "";
            context.Request.Headers.TryGetValue(HttpClientConsts.HEADER_AUTHORIZATION, out sToken);
            this.token = sToken.ToString();
        }

    }
}
