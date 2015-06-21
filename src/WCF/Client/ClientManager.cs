using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Contracts;

namespace WCF.Client
{
    public class ClientManager
    {
        public static void UploadVideo(string filename)
        {
            ChannelFactory<IUpload> httpFactory =
              new ChannelFactory<IUpload>(
                new BasicHttpBinding(),
                new EndpointAddress(
                  "http://localhost:988/Upload"));

            IUpload httpProxy =
              httpFactory.CreateChannel();

            httpProxy.UploadVideo(filename);
        }
    }
}

