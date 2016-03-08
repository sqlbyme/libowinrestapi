using Owin;
using System;
using System.Web.Http;
using Microsoft.Owin.Hosting;
namespace libOwinRestApi
{

    public class myEventArgs : EventArgs
    {
        public object argObject { get; set; }
    }

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
            myEventArgs args = new myEventArgs();
            return RESTEvent.e_RESTEvent.OnGetRequested(this, args);
        }

        // GET encoders/1
        //public object Get(int id)
        //{

        //}
    }

    public class RESTEvent
    {
        public static RESTEvent e_RESTEvent = new RESTEvent();

        public delegate object ReturnGetRequestHandler(object sender, myEventArgs args);

        public event ReturnGetRequestHandler getRequested;

        public object OnGetRequested(object sender, myEventArgs args)
        {
            ReturnGetRequestHandler getRequestedEvent = getRequested;
            if (getRequestedEvent != null)
            {
                    return getRequestedEvent.Invoke(this, args);
            }
            return args;
        }

        public void start()
        {
            // We have to leave the base url hard coded because of Windows 
            // lame security model surrounding binding to addresses 
            // and ports.  So we listen to all available ip's on
            // port 9000.
            string baseAddress = "http://+:9000/";
            WebApp.Start<OwinRestApi>(baseAddress);
            Console.WriteLine("Starting Server at: " + baseAddress);
        }

    }
}
