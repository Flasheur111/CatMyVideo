using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Storage.MongoFS
{
    public class Starter
    {
        private Process serverProcess;

        public Starter()
        {
            this.serverProcess = new Process();
            string dbDir = ConfigurationManager.AppSettings["mongoDir"];
            string argument = "--dbpath \"" + dbDir + "\"";
            this.serverProcess.StartInfo = new ProcessStartInfo("mongod.exe", argument);
        }

        public void Start()
        {
            serverProcess.Start();
        }

        public void Stop()
        {
            serverProcess.Kill();
        }
    }
}
