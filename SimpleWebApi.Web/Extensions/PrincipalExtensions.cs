using SimpleWebApi.Core.Models.Authorization;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace SimpleWebApi.Web.Extensions
{
    public static class PrincipalExtensions
    {
        public static AccessTokenData GetTokenData(this IPrincipal user)
        {
            var claims = (user as ClaimsPrincipal)?.Claims;

            if (claims != null)
            {
                return new AccessTokenData
                {
                    Username = claims.Single(x => x.Type == nameof(AccessTokenData.Username)).Value,
                    UserKey = Guid.Parse(claims.Single(x => x.Type == nameof(AccessTokenData.UserKey)).Value),
                    ExpireDate = DateTime.Parse(claims.Single(x => x.Type == nameof(AccessTokenData.ExpireDate)).Value)
                };
            }

            return null;
        }
    }
}