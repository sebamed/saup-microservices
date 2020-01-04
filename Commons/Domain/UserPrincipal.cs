using Commons.Consts;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Commons.Domain {
    public class UserPrincipal {

        private ClaimsPrincipal _principal;

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

        public UserPrincipal(ClaimsPrincipal principal) {
            this._principal = principal;
        }

    }
}
