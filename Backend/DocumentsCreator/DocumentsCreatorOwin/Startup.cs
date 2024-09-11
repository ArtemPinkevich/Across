using System.Web.Http;
using Owin;

namespace DocumentsCreatorOwin
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            httpConfiguration.Routes.MapHttpRoute( 
                name: "DefaultApi", 
                routeTemplate: "api/{controller}/{id}", 
                defaults: new { id = RouteParameter.Optional } 
            ); 
            builder.UseWebApi(httpConfiguration);
        }
    }
}