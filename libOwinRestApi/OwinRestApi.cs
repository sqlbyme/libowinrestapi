using Owin;
using System;
using System.Web.Http;
using System.Collections.Generic;
using Microsoft.Owin.Hosting;
namespace libOwinRestApi
{
    public class myEventArgs : EventArgs
    {
        public string argValue { get; set; }
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
        public string MyString { get; set; }

        // GET encoders 
        public IEnumerable<string> Get()
        {
            return new string[] { "Nothing to see here!" };
        }

        // GET encoders/1
        public string Get(int id)
        {
            myEventArgs args = new myEventArgs();
            args.argValue = id.ToString();
            EventSender myEventSender = new EventSender();
            myEventSender.StringReturnEvent += MyEventSender_StringReturnEvent;
            myEventSender.OnStringReturnEvent(this, args);
            return args.argValue;
        }

        private void MyEventSender_StringReturnEvent(object sender, myEventArgs args)
        {
            Console.WriteLine("Event fired within dll.");
            Console.WriteLine($"Event args recvd:{args.argValue.ToString()}");
        }
    }

    public class EventSender
    {
        

        // the delegate
        public delegate void ReturnStringEventHandler(object sender, myEventArgs args);

        // the event
        public event ReturnStringEventHandler StringReturnEvent;

        // raise the event
        public virtual void OnStringReturnEvent(object sender, myEventArgs args)
        {
            ReturnStringEventHandler myEvent = StringReturnEvent;
            if (myEvent != null)
            {
                foreach (Delegate d in myEvent.GetInvocationList())
                {
                    Console.WriteLine(d.Method.Name.ToString());
                    myEvent(this, args);
                }
                
            }
            else
            {
                Console.WriteLine(args.argValue.ToString());
            }
        }

        public void start(myEventArgs args)
        {
            string baseAddress = "http://localhost:9001/";
            WebApp.Start<OwinRestApi>(baseAddress);
            Console.WriteLine("Starting Server at: " + baseAddress);
            OnStringReturnEvent(this, args);

        }

        public void trigger(myEventArgs args)
        {
            OnStringReturnEvent(this, args);
        }
    }
}
