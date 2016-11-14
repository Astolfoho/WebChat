using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR;
using Chat.Api.Hubs;

[assembly: OwinStartup(typeof(Chat.Startup))]

namespace Chat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            GlobalConfiguration.Configure(ConfigureApi);
            ConfigureSignalar(app);
        }

        private static void ConfigureSignalar(IAppBuilder app)
        {
            app.MapSignalR();
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new SignalRContractResolver();
            var serializer = JsonSerializer.Create(settings);
            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => serializer);
        }

        private void ConfigureApi(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "Api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
    }
}
