using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebApi.Web.Utilities
{
    public class CryptoHelper
    {
        public static Tuple<string, string> HashPassword(string password)
        {
            var securityKey = MF.Dev.Crypto.Crypto.Create(password);

            return new Tuple<string, string>(securityKey.SaltedKeyword, securityKey.Id.ToString());
        }

        public static bool IsAuthorized(string password, string passwordSalt, string passwordHash)
        {
            if (Guid.TryParse(passwordSalt, out Guid saltGuid))
            {
                return MF.Dev.Crypto.Crypto.isMatch(password, saltGuid, passwordHash);
            }

            return false;
        }
    }
}