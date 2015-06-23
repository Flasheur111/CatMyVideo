using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Storage.WCF.Contracts
{
    [ServiceContract]
    public interface IUpload
    {
        [OperationContract]
        void UploadVideo(RemoteFileInfo file);
    }

    [MessageContract]
    public class RemoteFileInfo : IDisposable
    {
        [MessageHeader]
        public string FileName;

        [MessageHeader]
        public int ContentLength;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream Stream;

        public void Dispose()
        {
            if (Stream != null)
            {
                Stream.Close();
                Stream = null;
            }
        }
    }
}
