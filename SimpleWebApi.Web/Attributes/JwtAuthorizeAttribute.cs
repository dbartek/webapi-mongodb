using SimpleWebApi.Core.Models.Authorization;
using SimpleWebApi.Core.Utilities;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SimpleWebApi.Web.Attributes
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly ITokenProvider _tokenProvider;

        public JwtAuthorizeAttribute()
        {
            _tokenProvider = Bootstrapper.Resolve<ITokenProvider>();
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                var token = GetToken(actionContext.Request);

                if (token == null) throw new UnauthorizedAccessException();

                actionContext.RequestContext.Principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(nameof(token.Username), token.Username),
                    new Claim(nameof(token.UserKey), token.UserKey.ToString()),
                    new Claim(nameof(token.ExpireDate), token.ExpireDate.ToString())
                }));
            }
            catch (Exception)
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        private AccessTokenData GetToken(HttpRequestMessage request)
        {
            var authHeader = request?.Headers?.Authorization;
            if (authHeader?.Scheme != "Bearer") return null;

            var token = _tokenProvider.DecodeToken(authHeader?.Parameter);
            if (token.ExpireDate < DateTime.Now) return null;

            return token;
        }
    }
}