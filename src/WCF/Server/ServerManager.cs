using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Contracts;
using WCF.Definition;

namespace WCF.Server
{
    public class ServerManager
    {
        public static void RunServer()
        {
            using (ServiceHost host = new ServiceHost(
       typeof(Upload),
       new Uri[]{
          new Uri("http://localhost:988"),
        }))
            {
                host.AddServiceEndpoint(typeof(IUpload),
                  new BasicHttpBinding(),
                  "Upload");

                host.Open();

                Console.WriteLine("Service is available. " +
                  "Press <ENTER> to exit.");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}

