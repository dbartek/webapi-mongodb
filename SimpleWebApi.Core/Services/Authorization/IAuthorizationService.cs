using SimpleWebApi.Core.Models.Authorization;

namespace SimpleWebApi.Core.Services.Authorization
{
    public interface IAuthorizationService
    {
        AccessTokenResponse Login(LoginRequest model);
        bool Register(RegisterRequest model);
    }
}
