using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WpfApp.Web;
using static WpfApp.Web.WebServer;

namespace WpfAppConsole
{
    internal static class WebServerTest
    {
        public static void Run()
        {
            var server = new WebServer(8080);
            server.RequestReceived += OnRequestReseived;

            server.Start();

            Console.WriteLine("Сервер запущен");
            Console.ReadLine();
        }

        private static void OnRequestReseived(object sender, RequestReceiverEventArgs e)
        {
            var context = e.Context;

            Console.WriteLine("Connection {0}", context.Request.UserHostAddress);

            using var writer = new StreamWriter(context.Response.OutputStream);
            writer.WriteLine("Hello from Test Web Server!");
        }
    }
}
