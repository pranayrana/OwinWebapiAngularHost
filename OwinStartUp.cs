using Owin;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Extensions;
using System.Web.Http;

namespace WebApiHost
{
    public class OwinStartUp
    {
        public void Configuration(IAppBuilder appBuilder)
        {

            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // Configure Web API for self-host.

            HttpConfiguration config = new HttpConfiguration();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MapHttpAttributeRoutes();
            appBuilder.UseWebApi(config);



            //hosting static files i.e. angular
            var options = new FileServerOptions();
            options.EnableDirectoryBrowsing = true;
            options.FileSystem = new PhysicalFileSystem("./app");
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            //RequestPath = new PathString(string.Empty),
            appBuilder.UseFileServer(options);
            appBuilder.UseStageMarker(PipelineStage.MapHandler);

        }
    }
}
