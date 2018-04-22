using JWT.Algorithms;
using JWT.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleWebApi.Core.Models.Authorization;
using System;

namespace SimpleWebApi.Core.Utilities
{
    public class TokenProvider : ITokenProvider
    {
        private readonly AppConfiguration _appConfiguration;

        public TokenProvider(AppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        private string JwtSecret => _appConfiguration.JwtSecretKey;

        public string GenerateToken(AccessTokenData accessTokenData)
        {
            return new JwtBuilder()
              .WithAlgorithm(new HMACSHA256Algorithm())
              .WithSecret(JwtSecret)
              .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(2).ToUnixTimeSeconds())
              .AddClaim("username", accessTokenData.Username)
              .AddClaim("userKey", accessTokenData.UserKey)
              .Build();
        }

        public AccessTokenData DecodeToken(string tokenValue)
        {
            var json = new JwtBuilder()
                .WithSecret(JwtSecret)
                .MustVerifySignature()
                .Decode(tokenValue);

            var jsonObject = JObject.Parse(json);

            return new AccessTokenData
            {
                UserKey = Guid.Parse(jsonObject.GetValue("userKey").Value<string>()),
                Username = jsonObject.GetValue("username").Value<string>(),
                ExpireDate = DateTimeOffset.FromUnixTimeSeconds(jsonObject.GetValue("exp").Value<long>())
            };
        }
    }
}