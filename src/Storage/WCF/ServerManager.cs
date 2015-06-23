using Storage.MongoFS;
using Storage.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Storage.WCF
{
    public class ServerManager
    {
        public static void RunServer()
        {
            using (ServiceHost host = new ServiceHost(typeof(Upload), new Uri[] { new Uri("http://localhost:988") }))
            {
                var binding = new BasicHttpBinding();
                binding.MaxReceivedMessageSize = 2147483647;
                binding.ReaderQuotas.MaxStringContentLength = 2147483647;
                host.AddServiceEndpoint(typeof(IUpload), binding, "Upload");

                var mongoInstance = new Storage.MongoFS.Starter();
                mongoInstance.Start();
                var mongoDB = new Driver();

                host.Open();

                Console.WriteLine("Service is available. " +
                  "Press <ENTER> to exit.");
                Console.ReadLine();

                mongoInstance.Stop();
                host.Close();
            }
        }
    }
}

