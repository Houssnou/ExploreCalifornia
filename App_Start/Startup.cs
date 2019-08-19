using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using ExploreCalifornia.Config;
using ExploreCalifornia.ExceptionHandlers;
using ExploreCalifornia.Filters;
using ExploreCalifornia.Loggers;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(ExploreCalifornia.Startup))]
namespace ExploreCalifornia
{
    public class Startup
    {
        public static HttpConfiguration HttpConfiguration { get; set; } = new HttpConfiguration();

        public void Configuration(IAppBuilder app)
        {
            var config = Startup.HttpConfiguration;

            var json = config.Formatters.JsonFormatter.SerializerSettings;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            ConfigureWebApi(app, config);
            ConfigureSwashbuckle(config);
            ConfigureJwt(app);
        }

        public void ConfigureJwt(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new [] {GlobalConfig.Audience},
                IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                {
                    new SymmetricKeyIssuerSecurityKeyProvider(GlobalConfig.Audience, GlobalConfig.Secret)
                }
            });
        }
        
        private static void ConfigureWebApi(IAppBuilder app, HttpConfiguration config)
        {
            config.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHandler());

            config.MessageHandlers.Add(new TokenValidationHandler());

            config.Filters.Add(new DBUpdateExceptionFilterAttribute());

            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }

        public void ConfigureSwashbuckle(HttpConfiguration config)
        {
            config.EnableSwagger(c => {
                c.SingleApiVersion("v1", "ExploreCalifornia API");
                c.IncludeXmlComments($"{AppDomain.CurrentDomain.BaseDirectory}\\bin\\ExploreCalifornia.xml");
                })
                  .EnableSwaggerUi();
        }
    }
}