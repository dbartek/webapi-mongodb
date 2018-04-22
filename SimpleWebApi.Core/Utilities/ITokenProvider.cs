using SimpleWebApi.Core.Models.Authorization;

namespace SimpleWebApi.Core.Utilities
{
    public interface ITokenProvider
    {
        string GenerateToken(AccessTokenData accessTokenData);
        AccessTokenData DecodeToken(string tokenValue);
    }
}
