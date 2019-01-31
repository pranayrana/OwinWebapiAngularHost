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

            //hosting static files i.e. angular
            //install-package Microsoft.Owin.SelfHost
            //install-package Microsoft.Owin.StaticFiles
            var options = new FileServerOptions();
            options.EnableDirectoryBrowsing = true;
            options.FileSystem = new PhysicalFileSystem("./app");
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            appBuilder.UseFileServer(options);


            // Configure Web API for self-host.
            //Install-Package Microsoft.AspNet.WebApi.OwinSelfHost
            HttpConfiguration config = new HttpConfiguration();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MapHttpAttributeRoutes();
            appBuilder.UseWebApi(config);
            

            //Install-Package Microsoft.Owin.Cors
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }
    }
}
