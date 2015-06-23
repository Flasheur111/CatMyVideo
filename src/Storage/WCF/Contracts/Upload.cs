using Storage.MongoFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.WCF.Contracts
{
    class Upload : IUpload
    {
        public Driver mongo { get; set; }

        public Upload()
        {
            mongo = new Driver();
        }
        public void UploadVideo(RemoteFileInfo file)
        {
            Driver driverMongo = new Driver();
            driverMongo.UploadStream(file.Stream, file.FileName);
            driverMongo.ListFiles();
            
            Console.WriteLine("Upload Video :" + file.FileName); 
        }
    }
}
