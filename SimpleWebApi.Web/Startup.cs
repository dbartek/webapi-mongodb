using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(SimpleWebApi.Web.Startup))]

namespace SimpleWebApi.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
            Bootstrapper.Register(config);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            SwaggerConfig.Register(config);
        }
    }
}
