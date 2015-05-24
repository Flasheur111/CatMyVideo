using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    public class Starter
    {
        private Process serverProcess;

        public Starter()
        {
            this.serverProcess = new Process();
            this.serverProcess.StartInfo = new ProcessStartInfo("mongod.exe");
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
