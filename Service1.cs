using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WebApiHost
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            server = WebApp.Start<OwinStartUp>(baseAddress);
        }

        protected override void OnStop()
        {
            server.Dispose();
        }

        private IDisposable server;
        string baseAddress = "http://*:8081/";

        internal void TestStartup(string[] args)
        {
            this.OnStart(args);
        }

        internal void TestStop()
        {
            this.OnStop();
        }
    }
}

