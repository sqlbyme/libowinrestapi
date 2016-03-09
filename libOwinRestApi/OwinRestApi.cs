using Owin;
using System;
using System.Web.Http;
using Microsoft.Owin.Hosting;
namespace libOwinRestApi
{

    public class OwinRestApi
    {

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }

    }

    public class EncodersController : ApiController
    {
        // GET encoders 
        public object Get()
        {
            RESTEventArgs args = new RESTEventArgs();
            return RESTEvent.e_RESTEvent.OnGetRequested(this, args);
        }

        // GET encoders/<int value>
        public object Get(int id)
        {
            RESTEventArgs args = new RESTEventArgs();
            return RESTEvent.e_RESTEvent.OnGetRequested(id, args);
        }
    }

    public class RESTEventArgs : EventArgs
    {
        public object response { get; set; }
    }

    public class RESTEvent
    {
        public static RESTEvent e_RESTEvent = new RESTEvent();

        public delegate object ReturnGetRequestHandler(object sender, RESTEventArgs args);

        public event ReturnGetRequestHandler getRequested;

        public object OnGetRequested(object sender, RESTEventArgs args)
        {
            ReturnGetRequestHandler getRequestedEvent = getRequested;
            if (getRequestedEvent != null)
            {
                    return getRequestedEvent.Invoke(sender, args);
            }
            return args;
        }
    }

    public class ApiServer
    {
        private IDisposable myServer;

        public void ServerStart()
        {
            // We have to leave the base url hard coded because of Windows 
            // lame security model surrounding binding to addresses 
            // and ports.  So we listen to all available ip's on
            // port 9000.
            string baseAddress = "http://+:9000/";
            myServer = WebApp.Start<OwinRestApi>(baseAddress);
        }

        public void ServerStop()
        {
            myServer.Dispose();
        }
    }
}
