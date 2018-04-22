using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApi.Core
{
    public class AppConfiguration
    {
        public string DbConnectionString { get; set; }
        public string JwtSecretKey { get; set; }
    }
}
