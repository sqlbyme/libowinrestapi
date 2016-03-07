using System;
using Microsoft.Owin.Hosting;
using libOwinRestApi;

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9001/";

            WebApp.Start<OwinRestApi>(url: baseAddress);
            Console.WriteLine($"Starting Server at: {baseAddress}");
            Console.ReadLine();
        }
    }
}
