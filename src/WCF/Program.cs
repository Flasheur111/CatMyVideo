using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCF.Server;

namespace WCFServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerManager.RunServer();
        }
    }
}
