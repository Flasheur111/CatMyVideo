using Storage.MongoFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

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
            Engine.Dbo.Video video = new Engine.Dbo.Video() { Description = "Test", Title = "Test", UploadDate = DateTime.Now, ViewCount = 0, Encodes = null };
            Engine.BusinessManagement.Video.AddVideo(video);
            
            Console.WriteLine("Upload Video :" + file.FileName); 
        }
    }
}
