using System;

namespace SimpleWebApi.Core.Models.Authorization
{
    public class AccessTokenData
    {
        public string Username { get; set; }
        public Guid UserKey { get; set; }
        public DateTimeOffset ExpireDate { get; set; }
    }
}