using SimpleWebApi.Core.Models.Authorization;
using SimpleWebApi.Core.Services.Authorization;
using SimpleWebApi.Web.Attributes;
using System.Web.Http;

namespace SimpleWebApi.Web.Controllers
{
    [ErrorFilter]
    [RoutePrefix("api/auth")]
    public class AuthorizationController : ApiController
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Authorize user by username and password
        /// </summary>
        /// <param name="model"></param>
        /// <returns>JWT token</returns>
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginRequest model)
        {
            return Json(_authorizationService.Login(model));
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(RegisterRequest model)
        {
            if(_authorizationService.Register(model))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
