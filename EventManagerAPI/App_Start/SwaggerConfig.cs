using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
namespace EventManagerAPI.App_Start
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "EventManagerAPI");
                    c.IncludeXmlComments(GetXmlCommentsPath()); // Optional: if you want to include XML comments
                })
                .EnableSwaggerUi();
        }

        private static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\EventManagerAPI.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}