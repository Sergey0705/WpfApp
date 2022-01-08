using System;
using System.IO;
using WpfApp.Services.Interfaces;
using WpfApp.Web;

namespace WpfApp.Services
{
    internal class HttpListenerWebService : IWebServerService
    {
        private WebServer _Server = new WebServer(8080);
        public bool Enabled { get => _Server.Enabled; set => _Server.Enabled = value; }

        public void Start() => _Server.Start();
  
        public void Stop() => _Server.Stop();

        public HttpListenerWebService()
        {
            _Server.RequestReceived += OnRequestReceived;
        }

        private void OnRequestReceived(object sender, WebServer.RequestReceiverEventArgs e)
        {
            using var writer = new StreamWriter(e.Context.Response.OutputStream);
            writer.WriteLine("WpfApp Application" + DateTime.Now);
        }
    }
}
