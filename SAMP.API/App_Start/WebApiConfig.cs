using System.Web.Http;
using System.Web.Http.Cors;

namespace SAMP.API
{
    /// <summary>
    /// WebApiConfig class
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register
        /// </summary>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "Api/Login/Get",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
