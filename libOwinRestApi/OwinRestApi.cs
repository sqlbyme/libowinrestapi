using Owin;
using System;
using System.Web.Http;
using System.Collections.Generic;
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

    public class myClass { public string name { get; set; } public int myId { get; set; } }

    public class EncodersController : ApiController
    {
        // GET encoders 
        public object Get()
        {
            myClass clsMine = new myClass();
            clsMine.name = "Bob";
            clsMine.myId = 100;
            myEventArgs args = new myEventArgs();
            args.argObject = clsMine;
            EventSender.myEventSender.OnStringReturnEvent(this, args);
            //string[] arrEncoders = new string[] {"Channel 1", "Channel 2", "Channel 3", "Channel 4"};
            return clsMine;
        }

        // GET encoders/1
        public object Get(int id)
        {
            myClass clsMine = new myClass();
            clsMine.myId = id;
            myEventArgs args = new myEventArgs();
            args.argObject = clsMine;
            EventSender.myEventSender.OnStringReturnEvent(this, args);
            return args.argObject;
        }
    }

    public class EventSender
    {
        public static EventSender myEventSender = new EventSender();

        // the delegate
        public delegate void ReturnStringEventHandler(object sender, myEventArgs args);

        // the event
        public event ReturnStringEventHandler StringReturnEvent;

        // raise the event
        public void OnStringReturnEvent(object sender, myEventArgs args)
        {
            ReturnStringEventHandler myEvent = StringReturnEvent;
            if (myEvent != null)
            {
                    myEvent.Invoke(this, args);
            }
        }

        public void start(myEventArgs args)
        {
            string baseAddress = "http://localhost:9001/";
            WebApp.Start<OwinRestApi>(baseAddress);
            Console.WriteLine("Starting Server at: " + baseAddress);
            OnStringReturnEvent(this, args);

        }

    }
}
